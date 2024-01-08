using Azure.Core;
using POS.Core.DTO;
using POS.DB;
using POS.DB.Enums;
using POS.DB.Models;
using Item = POS.DB.Models.Item;
using Tax = POS.DB.Models.Tax;

namespace POS.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public Order? CreateNewOrder(CreateOrderRequest request)
        {
            // Find if customer exist 
            var customer = _context.User.Find(request.CustomerId);
            if (customer == null)
            {
                return null;
            }

            // Find employee  
            var employee = _context.User.Find(request.EmployeeId);
            if (employee == null)
            {
                return null;
            }
            // Check if they are really an employee
            if (employee.BusinessId == null)
            {
                return null;
            }

            DateTime creationDate = (DateTime)(request.CreationDate == null ? DateTime.Now : request.CreationDate);
            var newOrder = new Order
            {
                TipAmount = request.TipAmount,
                CreationDate = creationDate,
                PendingUntil = GeneratePendingUntilDate(creationDate),
                Status = OrderStatus.Pending,
                PaymentMethod = PaymentMethod.None,
                Customer = customer,
                CustomerId = request.CustomerId,
                Employee = employee,
                EmployeeId = request.EmployeeId
            };
            _context.Orders.Add(newOrder);

            Item item;
            Tax? tax;
            OrderLine orderLine;
            int currLineId = 0;
            if (request.OrderLines != null && request.OrderLines.Any())
            {
                // The request is invalid if same item is listed multiple times
                if (HasItemDuplicates(request.OrderLines))
                {
                    return null;
                }

                foreach (var line in request.OrderLines)
                {
                    if (line.UnitCount <= 0)
                    {
                        return null;
                    }

                    // Validating Item
                    item = _context.Items.Find(line.ItemId);
                    if (item == null)
                    {
                        return null;
                    }
                    if (item.IsUnavailable)
                    {
                        return null;
                    }

                    // Validating tax
                    if (line.TaxId != null)
                    {
                        tax = _context.Tax.Find(line.TaxId);
                        if (tax == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        tax = _context.Tax.Find(item.DefaultTaxId);
                        if (tax == null)
                        {
                            return null;
                        }
                    }


                    //TODO calculate discount stuff
                    orderLine = new OrderLine
                    {
                        OrderId = newOrder.Id,
                        LineId = currLineId,
                        UnitPrice = item.Price,
                        UnitCount = line.UnitCount,
                        Item = item,
                        ItemId = item.Id,
                        AppliedTax = tax,
                        AppliedTaxId = tax.Id,
                        Order = newOrder,
                    };

                    _context.OrdersLine.Add(orderLine);
                    if (newOrder.OrderLines == null)
                    {
                        newOrder.OrderLines = new List<OrderLine>();
                    }
                    newOrder.OrderLines.Add(orderLine);
                    currLineId += 1;
                }
            }

            _context.SaveChanges();
            return newOrder;
        }

        public bool CancelOrder(int orderId)
        {
            // Find employee  
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return false;
            }
            if (order.Status != OrderStatus.Pending)
            {
                return false;
            }

            // Clearing vouchers if they were used on the order before payment
            List<DB.Models.Voucher> vouchers = _context.Voucher.Where(i => i.IsUsed == true && i.OrderId == orderId).Select(i => i).ToList();
            foreach (var voucher in vouchers)
            {
                voucher.IsUsed = false;
                voucher.OrderId = null;
                voucher.Order = null;
            }

            order.Status = OrderStatus.Canceled;

            _context.SaveChanges();

            return true;
        }

        // General TODO: recheck for any discounts
        public Order? AddAdditionalItems(int orderId, List<CreateOrderLineRequest> request)
        {
            // Find employee  
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return null;
            }
            if (order.Status != OrderStatus.Pending)
            {
                return null;
            }

            Item? item;
            Tax? tax = null;
            OrderLine? orderLine = null;

            // Getting already existing order lines
            List<OrderLine>? orderLineList = _context.OrdersLine.Where(i => i.OrderId == orderId).Select(i => i).ToList();
            int highestLineId = 0;
            if (orderLineList != null && orderLineList.Any())
            {
                highestLineId = orderLineList.OrderByDescending(orderLine => orderLine.LineId).FirstOrDefault().LineId + 1;
            }

            // Request is not empty and do not contains duplicate items
            if (request != null && request.Any() && !HasItemDuplicates(request))
            {
                foreach (var line in request)
                {
                    if (line.UnitCount <= 0)
                    {
                        return null;
                    }

                    // Validating Item
                    item = _context.Items.Find(line.ItemId);
                    if (item == null)
                    {
                        return null;
                    }
                    if (item.IsUnavailable)
                    {
                        return null;
                    }

                    // Validating tax
                    if (line.TaxId != null)
                    {
                        tax = _context.Tax.Find(line.TaxId);
                        if (tax == null)
                        {
                            return null;
                        }
                    }

                    // Checking agains existing order lines
                    if(orderLineList != null)
                        orderLine = orderLineList.FirstOrDefault(item => item.ItemId == line.ItemId);

                    if (orderLine != null) // If found existing item - update
                    {
                        orderLine.UnitCount = line.UnitCount;
                        if (tax != null)
                        {
                            orderLine.AppliedTax = tax;
                            orderLine.AppliedTaxId = tax.Id;
                        }
                    }
                    else // Add new order line
                    {
                        if (line.TaxId == null)
                        {
                            tax = _context.Tax.Find(item.DefaultTaxId);
                            if (tax == null)
                            {
                                return null;
                            }
                        }

                        orderLine = new OrderLine
                        {
                            OrderId = order.Id,
                            LineId = highestLineId,
                            UnitPrice = item.Price,
                            UnitCount = line.UnitCount,
                            Item = item,
                            ItemId = item.Id,
                            AppliedTax = tax,
                            AppliedTaxId = tax.Id,
                            Order = order,
                        };
                        highestLineId += 1;

                        _context.OrdersLine.Add(orderLine);
                        if (order.OrderLines == null)
                        {
                            order.OrderLines = new List<OrderLine>();
                        }
                        order.OrderLines.Add(orderLine);
                    }

                    tax = null;
                    orderLine = null;
                }
            }
            else
            {
                return null;
            }

            _context.SaveChanges();
            return order;
        }

        public bool RemoveItems(int orderId, List<int> itemIds)
        {
            // Find employee  
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return false;
            }
            if (order.Status != OrderStatus.Pending)
            {
                return false;
            }

            if (itemIds == null || !itemIds.Any())
            {
                return false;
            }


            List<OrderLine> orderLineListToRemove = _context.OrdersLine
            .Where(i => i.OrderId == orderId && itemIds.Contains(i.ItemId))
            .Select(i => i)
            .ToList();

            foreach (var toRemoveLine in orderLineListToRemove)
            {
                _context.OrdersLine.Remove(toRemoveLine);
            }

            _context.SaveChanges();

            return true;
        }

        // Helper functions
        public DateTime GeneratePendingUntilDate(DateTime creationDate) //TODO: Add actuall pending time
        {
            return creationDate.AddDays(7);
        }

        public static bool HasItemDuplicates(List<CreateOrderLineRequest> list)
        {
            var uniqueItems = new HashSet<int>();
            return list.Any(item => !uniqueItems.Add(item.ItemId));
        }
    }
}
