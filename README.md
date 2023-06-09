# Welcome to MoMoSdk!

MoMoSdk is a library for interact with MoMo . wallet APIs


# Setting

following `appsettings.json` file:

    {   
          "MoMoPayment": {  
        	  "Endpoint": "https://test-payment.momo.vn/v2/gateway/api",  
        	  "AccessKey": "mTCKt9W3eU1m39TW",  
        	  "SecretKey": "SetA5RDnLHvt51AULf51DyauxUo3kDU6",  
        	  "PartnerCode": "MOMOLRJZ20181206",  
        	  "PartnerName": "PRUgift",  
        	  "StoreId": "",  
        	  "RedirectUrl": "https://localhost:7244/Redirect",  
        	  "IpnUrl": "https://3525-14-231-221-55.ap.ngrok.io/api/InstantPaymentNotification"  
          }  
    }

**Endpoint**: MoMo's API
**AccessKey**: your AccessKey
**SecretKey**: your SecretKey
**PartnerCode**: your PartnerCode
**PartnerName**: your PartnerName
**StoreId**: your StoreId
**RedirectUrl**: A partner's URL. This URL is used to redirect from MoMo page to partner's page after customer's payment.
**IpnUrl**: Partner API. Used by MoMo to submit payment results by method (server-to-server) method

## Config
add Setting into Program.cs

    builder.Services.AddMoMoSdk();

## Create Order
add MoMo service

    private readonly IMoMoService _momoService;

crete model:

    public class CreateOrderViewModel  
    {  
        public MoMoRequestType RequestType { get; set; }  
        public string OrderId { get; set; }  
        public string OrderInfo { get; set; }  
        public long Amount { get; set; }  
          
        public DeliveryInfo DeliveryInfo { get; set; }  
          
        public UserInfo UserInfo { get; set; }  
    }

create order

    PaymentResponse paymentResponse = await _momoService.CreatePayment( model.RequestType,model.OrderId,model.Amount,model.OrderInfo,MoMoCodeHelper.Base64Encode("{}"),new List<Item>(),model.DeliveryInfo,model.UserInfo);

## Query Order

    QueryResponse queryResponse = await _momoService.QueryPayment(orderId);

## Handle Redirect

    public void Index([FromQuery] RedirectQuery query)  
    {  
        if (_momoService.CheckRedirectUrl(query))  
        {  
            //do somethings here
       }  
      
       throw new Exception("Wrong Signature from MoMo Server");  
    }

## Handle Instant Payment Notification

    [HttpPost]  
    public StatusCodeResult Post([FromBody] IPNModel model)  
    {  
        if (_momoService.CheckIPN(model))  
        {  
            //do somethings here
      }  
        return BadRequest();  
    }

## Create Refund

Refund Model

    public class RefundViewModel  
    {  
        public string OrderId { get; set; }  
        public long Amount { get; set; }  
        public long TransId { get; set; }  
        public string? Description { get; set; }  
    }

create request refund

    RefundResponse paymentResponse = await _momoService.CreateRefund(model.OrderId,model.Amount,model.TransId,model.Description);


# Query Refund


    QueryRefundResponse  queryRefundResponse = await _momoService.QueryRefund(orderId);
