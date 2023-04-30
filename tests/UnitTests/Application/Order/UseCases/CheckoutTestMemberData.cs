﻿namespace UnitTests.Application.Order.UseCases;

public class CheckoutTestMemberData
{
    public static IEnumerable<object[]> GetCuponsForTest()
    {
        yield return new object[] { "VALE20", 240 };
        yield return new object[] { "VALE10", 270 };
    }
}