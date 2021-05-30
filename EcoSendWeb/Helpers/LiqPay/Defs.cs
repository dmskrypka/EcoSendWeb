namespace EcoSendWeb.Helpers.LiqPay
{
    public enum LiqpayActions
    {
        pay,
        hold,
        paysplit,
        subscribe,
        paydonate,
        auth,
        regular
    }

    public enum LiqpayPayTypes
    {
        Card,
        Liqpay,
        Privat24,
        Masterpass,
        MomentPart,
        Cash,
        Invoice,
        QR
    }

    public enum LiqpayStatuses
    {
        Error,
        Failure,
        Reversed,
        Subscribed,
        Success,
        Unsubscribed,
        ThreeDsVerify,
        CaptchaVerify,
        CvvVerify,
        IvrVerify,
        OtpVerify,
        PasswordVerify,
        PhoneVerify,
        PinVerify,
        ReceiverVerify,
        SenderVerify,
        SenderappVerify,
        WaitQR,
        WaitSender,
        CashWait,
        HoldWait,
        InvoiceWait,
        Prepared,
        Processing,
        WaitAccept,
        WaitCard,
        WaitCompensation,
        WaitLC,
        WaitReserve,
        WaitSecure
    }
}