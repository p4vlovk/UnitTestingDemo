﻿namespace DemoCode;

public interface IEmailGateway
{
    void Send(EmailMessage email);
}