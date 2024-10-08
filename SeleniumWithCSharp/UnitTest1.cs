﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumWithCSharp.Pages;

namespace SeleniumWithCSharp;

[TestFixture("https://google.com", "selenium")]
public class Tests
{
    IWebDriver? _driver = null;
    private readonly string _url;
    private readonly string _keyword;

    public Tests(string url,string keyword)
    {
        this._url = url;
        this._keyword = keyword;
    }
    
    [SetUp]
    public void Setup()
    {

        _driver = new ChromeDriver();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        SeleniumCustomMethods.MaximizeWindow(_driver);
        SeleniumCustomMethods.NavigateToUrl(_driver, _url);

    }

    [Test]
    public void CustomMethodImplWithPOM()
    {
        Console.WriteLine(SeleniumCustomMethods.GetTitle(_driver));
        var homePage = new HomePage(_driver);
        Assert.IsTrue(homePage.IsSearchAreaVisible().Item1);
        homePage.SearchKeywordOnGoogle(_keyword);
        
    }

    [Test]
    [Category("smoke")]
    [TestCase("chrome","30")]
    public void TestBrowserVersion(string browser,string version)
    {
        Console.WriteLine($"Browser : {browser}");

        var btn = _driver.FindElement(By.CssSelector("#login_button"), 10);
        btn.Click();
        var employeeLabel = _driver.FindElement(By.CssSelector("#VCC_VSL"), 10);
        Assert.AreEqual("Employee", employeeLabel.Text);


    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
    }
