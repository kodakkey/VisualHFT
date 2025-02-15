﻿using System.Collections.Generic;
using System.Linq;

namespace VisualHFT.Model
{
    public class ExecutionVM: OpenExecution
    {

        public ExecutionVM(OpenExecution exec, string symbol)
        {
            if (exec == null)
                return;
            this.ClOrdId = exec.ClOrdId;
            this.ExecID = exec.ExecID;
            this.ExecutionID = exec.ExecutionID;
            this.IsOpen = exec.IsOpen;
            this.LocalTimeStamp = exec.LocalTimeStamp;
            this.PositionID = exec.PositionID;
            this.Price = exec.Price;
            this.ProviderID = exec.ProviderID;
            this.QtyFilled = exec.QtyFilled;
            this.ServerTimeStamp = exec.ServerTimeStamp;
            this.Side = (ePOSITIONSIDE)exec.Side;
            this.Status = (ePOSITIONSTATUS)exec.Status;
            this.Symbol = symbol;
        }
        public ExecutionVM(CloseExecution exec, string symbol)
        {
            if (exec == null)
                return;
            this.ClOrdId = exec.ClOrdId;
            this.ExecID = exec.ExecID;
            this.ExecutionID = exec.ExecutionID;
            this.IsOpen = exec.IsOpen;
            this.LocalTimeStamp = exec.LocalTimeStamp;
            this.PositionID = exec.PositionID;
            this.Price = exec.Price;
            this.ProviderID = exec.ProviderID;
            this.QtyFilled = exec.QtyFilled;
            this.ServerTimeStamp = exec.ServerTimeStamp;
            this.Side = (ePOSITIONSIDE)exec.Side;
            this.Status = (ePOSITIONSTATUS)exec.Status;
            this.Symbol = symbol;
        }
        public string ProviderName { get; set; }
        public string Symbol { get; set; }
        public double LatencyInMiliseconds
        {
            get { return this.LocalTimeStamp.Subtract(this.ServerTimeStamp).TotalMilliseconds; }
        }
        public new ePOSITIONSIDE Side
        {
            get { return base.Side == null ? ePOSITIONSIDE.None : (ePOSITIONSIDE)base.Side; }
            set { base.Side = (int)value; }
        }
        public new ePOSITIONSTATUS Status
        {
            get { return base.Status == null? ePOSITIONSTATUS.NONE: (ePOSITIONSTATUS)base.Status; }
            set { base.Status = (int)value; }
        }
    }
    public class PositionEx : Position
    {
        /*
            No need to implement base fields notification methods, since this class won't be dynamic.
            It will be static collections, where only adding new items to the collection is the only thing dynamic.
        */
        public PositionEx()
        {
        }
        public PositionEx(Position p)
        {
            this.CloseExecutions = p.CloseExecutions.Select(x => new ExecutionVM(x, p.Symbol)).ToList();
            this.OpenExecutions = p.OpenExecutions.Select(x => new ExecutionVM(x, p.Symbol)).ToList();


            this.AttemptsToClose = p.AttemptsToClose;
            this.CloseBestAsk = p.CloseBestAsk;
            this.CloseBestBid = p.CloseBestBid;
            this.CloseClOrdId = p.CloseClOrdId;
            this.CloseFireSignalTimestamp = p.CloseFireSignalTimestamp;
            this.CloseOriginPartyID = p.CloseOriginPartyID;
            this.CloseProviderId = p.CloseProviderId;
            this.CloseQuoteId = p.CloseQuoteId;
            this.CloseQuoteLocalTimeStamp = p.CloseQuoteLocalTimeStamp;
            this.CloseQuoteServerTimeStamp = p.CloseQuoteServerTimeStamp;
            this.CloseStatus = (ePOSITIONSTATUS)p.CloseStatus;
            this.CloseTimeStamp = p.CloseTimeStamp;
            this.CreationTimeStamp = p.CreationTimeStamp;
            this.Currency = p.Currency;
            this.FreeText = p.FreeText;
            this.FutSettDate = p.FutSettDate;
            this.GetCloseAvgPrice = p.GetCloseAvgPrice;
            this.GetCloseQuantity = p.GetCloseQuantity;
            this.GetOpenAvgPrice = p.GetOpenAvgPrice;
            this.GetOpenQuantity = p.GetOpenQuantity;
            this.GetPipsPnL = p.GetPipsPnL;
            this.ID = p.ID;
            this.IsCloseMM = p.IsCloseMM;
            this.IsOpenMM = p.IsOpenMM;
            this.LayerName = this.LayerName;
            this.MaxDrowdown = p.MaxDrowdown;
            this.OpenBestAsk = p.OpenBestAsk;
            this.OpenBestBid = p.OpenBestBid;
            this.OpenClOrdId = p.OpenClOrdId;
            this.OpenFireSignalTimestamp = p.OpenFireSignalTimestamp;
            this.OpenOriginPartyID = p.OpenOriginPartyID;
            this.OpenProviderId = p.OpenProviderId;
            this.OpenQuoteId = p.OpenQuoteId;
            this.OpenQuoteLocalTimeStamp = p.OpenQuoteLocalTimeStamp;
            this.OpenQuoteServerTimeStamp = p.OpenQuoteServerTimeStamp;
            this.OpenStatus = (ePOSITIONSTATUS)p.OpenStatus;
            this.OrderQuantity = p.OrderQuantity;
            this.PipsPnLInCurrency = p.PipsPnLInCurrency;
            this.PipsTrail = p.PipsTrail;
            this.PositionID = p.PositionID;
            this.Side = (ePOSITIONSIDE)p.Side;
            this.StopLoss = p.StopLoss;
            this.StrategyCode = p.StrategyCode;
            this.Symbol = p.Symbol;
            this.SymbolDecimals = p.SymbolDecimals;
            this.SymbolMultiplier = p.SymbolMultiplier;
            this.TakeProfit = p.TakeProfit;
            this.UnrealizedPnL = p.UnrealizedPnL;
        }
        ~PositionEx()
        {
            if (this.CloseExecutions != null)
                this.CloseExecutions.Clear();
            this.CloseExecutions = null;

            if (this.OpenExecutions != null)
                this.OpenExecutions.Clear();
            this.OpenExecutions = null;
        }
        public string OpenProviderName { get; set; }
        public string CloseProviderName { get; set; }

        public new List<ExecutionVM> OpenExecutions { get; set; }
        public new List<ExecutionVM> CloseExecutions { get; set; }

        public List<ExecutionVM> AllExecutions {
            get {
                var _ret = new List<ExecutionVM>();
                if (this.OpenExecutions != null && this.OpenExecutions.Any())
                    _ret.AddRange(this.OpenExecutions);

                if (this.CloseExecutions != null && this.CloseExecutions.Any())
                    _ret.AddRange(this.CloseExecutions);
                return _ret/*.OrderBy(x => x.ServerTimeStamp)*/.ToList();
            }
        }
        private OrderVM GetOrder(bool isOpen)
        {
            if (!string.IsNullOrEmpty(this.OpenClOrdId))
            {
                OrderVM o = new OrderVM();
                //o.OrderID
                o.Currency = this.Currency;
                o.ClOrdId = isOpen? this.OpenClOrdId : this.CloseClOrdId;
                o.ProviderId = isOpen ? this.OpenProviderId: this.CloseProviderId;
                o.ProviderName = isOpen ? this.OpenProviderName: this.CloseProviderName;
                o.LayerName = this.LayerName;
                o.AttemptsToClose = this.AttemptsToClose;
                o.BestAsk = isOpen ? this.OpenBestAsk.ToDouble(): this.CloseBestAsk.ToDouble();
                o.BestBid = isOpen ? this.OpenBestBid.ToDouble(): this.CloseBestBid.ToDouble();
                o.CreationTimeStamp = isOpen ? this.OpenQuoteLocalTimeStamp.ToDateTime(): this.CloseQuoteLocalTimeStamp.ToDateTime();
                o.Executions = isOpen ? this.OpenExecutions.ToList() : this.CloseExecutions.ToList();
                o.SymbolMultiplier = this.SymbolMultiplier;
                o.Symbol = this.Symbol;
                o.FreeText = this.FreeText;
                o.Status = (eORDERSTATUS)(isOpen ? this.OpenStatus: this.CloseStatus);
                o.GetAvgPrice = isOpen ? this.GetOpenAvgPrice.ToDouble(): this.GetCloseAvgPrice.ToDouble();

                o.GetQuantity = isOpen ? this.GetOpenQuantity.ToDouble(): this.GetCloseQuantity.ToDouble();
                o.Quantity = this.OrderQuantity.ToDouble();
                o.FilledQuantity = isOpen ? this.GetOpenQuantity.ToDouble() : this.GetCloseQuantity.ToDouble();
                
                o.IsEmpty = false;
                o.IsMM = isOpen ? this.IsOpenMM: this.IsCloseMM;
                //o.MaxDrowdown = 
                //o.MinQuantity = 
                //o.OrderID = 

                //TO-DO: we need to find a way to add this.
                //*************o.OrderType = this 

                //o.PipsTrail

                o.PricePlaced = o.Executions.Where(x => x.Status == ePOSITIONSTATUS.SENT || x.Status == ePOSITIONSTATUS.NEW || x.Status == ePOSITIONSTATUS.REPLACESENT)
                    .First().Price.ToDouble();
                if (o.PricePlaced == 0) //if this happens, is because the data is corrupted. But, in order to auto-fix it, we use AvgPrice
                {
                    o.PricePlaced = o.GetAvgPrice;
                }
                o.QuoteID = isOpen ? this.OpenQuoteId.ToInt(): this.CloseQuoteId.ToInt();
                o.QuoteLocalTimeStamp = isOpen ? this.OpenQuoteLocalTimeStamp.ToDateTime(): this.CloseQuoteLocalTimeStamp.ToDateTime();
                o.QuoteServerTimeStamp = isOpen ? this.OpenQuoteServerTimeStamp.ToDateTime(): this.CloseQuoteServerTimeStamp.ToDateTime();
                if (isOpen)
                    o.Side = (eORDERSIDE)this.Side;
                else
                    o.Side = (eORDERSIDE)(this.Side == ePOSITIONSIDE.Sell ? eORDERSIDE.Buy : eORDERSIDE.Sell); //the opposite
                //o.StopLoss = 
                o.StrategyCode = this.StrategyCode;
                o.SymbolDecimals = this.SymbolDecimals;
                o.SymbolMultiplier = this.SymbolMultiplier;
                //o.TakeProfit

                //TO-DO: we need to find a way to add this.
                //*************o.TimeInForce = 

                //o.UnrealizedPnL               
                o.LastUpdated = System.DateTime.Now;
                o.FilledPercentage = 100* (o.FilledQuantity / o.Quantity);
                return o;
            }
            return null;
        }
        public List<OrderVM> GetOrders()
        {

            OrderVM openOrder = GetOrder(true);
            OrderVM closeOrder = GetOrder(false);
            var orders = new List<OrderVM>();
            if (openOrder != null)
                orders.Add(openOrder);
            if (closeOrder != null)
                orders.Add(closeOrder);


            return orders;
        }


        public new ePOSITIONSIDE Side
        {
            get { return (ePOSITIONSIDE)base.Side; }
            set { base.Side = (int)value; }
        }
        public new ePOSITIONSTATUS CloseStatus
        {
            get { return (ePOSITIONSTATUS)base.CloseStatus; }
            set { base.CloseStatus = (int)value; }
        }
        public new ePOSITIONSTATUS OpenStatus
        {
            get { return (ePOSITIONSTATUS)base.OpenStatus; }
            set { base.OpenStatus = (int)value; }
        }

    }
}
