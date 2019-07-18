using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class ViewRequestDetail
    {
        int id;
        string description;
        int? qty;
        string unitOfMeasure;
        decimal? unitCost;
        decimal? totalPrice;

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public int? Qty { get => qty; set => qty = value; }
        public string UnitOfMeasure { get => unitOfMeasure; set => unitOfMeasure = value; }
        public decimal? UnitCost { get => unitCost; set => unitCost = value; }
        public decimal? TotalPrice { get => totalPrice; set => totalPrice = UnitCost * Qty; }

        public string Remarks { get; set; }
        public int getId()
        {
            return Id;
        }

        public string getDescription()
        {
            return Description;
        }

        public int? getQty()
        {
            return Qty;
        }

        public string getUnitOfMeasure()
        {
            return UnitOfMeasure;
        }

        public decimal? getUnitCost()
        {
            return UnitCost;
        }

        public decimal? getTotalPrice()
        {
            return TotalPrice;
        }

        public decimal? setTotalPrice()
        {
            return UnitCost * Qty;
        }
        public void setId(int id)
        {
            this.Id = id;
        }

        public void setUnitOfMeasure(string uom)
        {
            this.UnitOfMeasure = uom;
        }
        public void setUnitCost(decimal cost)
        {
            this.UnitCost = cost;
        }
        public void setTotalPrice(decimal price)
        {
            this.TotalPrice = price;
        }

        public ViewRequestDetail()
        {

        }

        public ViewRequestDetail(int id, string description, int? qty, string unitOfMeasure, decimal? unitCost, decimal? totalPrice, string remarks)
        {
            this.Id = id;
            this.Description = description;
            this.Qty = qty;
            this.UnitOfMeasure = unitOfMeasure;
            this.UnitCost = unitCost;
            this.TotalPrice = totalPrice;
            this.Remarks = remarks;
        }
        public ViewRequestDetail(int id, string description, int? qty, string unitOfMeasure, decimal? unitCost, decimal? totalPrice)
        {
            this.Id = id;
            this.Description = description;
            this.Qty = qty;
            this.UnitOfMeasure = unitOfMeasure;
            this.UnitCost = unitCost;
            this.TotalPrice = totalPrice;

        }
    }
}