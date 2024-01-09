﻿using Azure.Core;
using Microsoft.AspNet.Identity;
using POS.Core.DTO;
using POS.DB;
using POS.DB.Enums;
using POS.DB.Models;
using System.ComponentModel.DataAnnotations;
using Item = POS.DB.Models.Item;
using Tax = POS.DB.Models.Tax;
using Voucher = POS.DB.Models.Voucher;

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
        public bool ApplyVoucher(int orderId, int voucherId)
        {
            var voucher = _context.Voucher.Find(voucherId);
            if(voucher == null)
            {
                return false;
            }
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return false;
            }

            if (voucher.IsUsed)
            {
                return false;
            }

            // Check if the current date is within the validity period
            DateTime currentDateTime = DateTime.Now;
            if (currentDateTime < voucher.ValidFrom && currentDateTime > voucher.ValidTo)
            {
                return false; 
            }

            // Add order to voucher
            voucher.OrderId = orderId;
            voucher.Order = order;
            voucher.IsUsed = true;

            // Add voucher to order
            if (order.Vouchers == null)
            {
                order.Vouchers = new List<Voucher>();   
            }
            order.Vouchers.Add(voucher);

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

        public bool ApplyDiscount(int orderId, int discountId)
        {
            throw new NotImplementedException();
        }

        public InvoiceResponse? GetOrderInvoice(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null || order.PaidDate == null)
            {
                return null;
            }

            var items = new List<InvoiceItem>();
            if (order.OrderLines != null) {
                for (int i = 0; i < order.OrderLines.Count; i++)
                {
                    var tax = _context.Tax.Find(order.OrderLines[i].AppliedTaxId)?.AmountPct ?? 0;
                    var invoice_item = new InvoiceItem
                    {
                        UnitPrice = order.OrderLines[i].UnitPrice,
                        UnitCount = order.OrderLines[i].UnitCount,
                        ItemId = order.OrderLines[i].ItemId,
                        AppliedDiscount = 0, // FIXME: Calculate discount
                        AppliedTax = 0
                    };
                    invoice_item.AppliedTax = invoice_item.UnitPrice * invoice_item.UnitCount * tax;
                    items.Add(invoice_item);
                }
            }

            var TotalSum = items.Sum(item => item.UnitPrice * item.UnitCount + item.AppliedTax);

            return new InvoiceResponse
            {
                Id = order.Id,
                TotalSum = TotalSum,
                TipAmount = order.TipAmount,
                PaidDate = order.PaidDate.Value,
                PaymentMethod = order.PaymentMethod,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                BusinessId = order.Employee?.BusinessId,
                InvoiceItems = items
            };
        }
    }
}
