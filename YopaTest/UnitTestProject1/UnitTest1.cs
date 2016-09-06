using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1 //Click on Book Valuation using header link
    {
        public ChromeOptions options = new ChromeOptions();
        public IWebDriver driver;

        [TestMethod]
        public void TestMethod1()
        {
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver("C:\\temp\\SeleniumWebDriver", options);
            driver.Navigate().GoToUrl("http://qa.yopa.uk");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement bookVal = wait.Until(d => d.FindElement(By.CssSelector("#main-navigation > div > div.col-md-11.col-sm-12.hero--nav__wrapper > nav > ul > li:nth-child(4) > a")));

            //Click on link
            bookVal.Click();
            IWebElement stepone = wait.Until(d => d.FindElement(By.CssSelector("#step1")));
            Assert.AreEqual(stepone.Displayed, true);
            //IWebElement postCode = wait.Until(d => d.FindElement(By.ClassName("form-control postcode validate-field v3-input  data-validation-invalid")));
            IWebElement postCode = wait.Until(d => d.FindElement(By.Name("default_postcode")));
            Assert.AreEqual(postCode.Displayed, true);
            postCode.SendKeys("KT198TZ");
            Assert.AreEqual(postCode.GetAttribute("value"), "KT198TZ");
            
            // not perfect but sendkeys doesnt allow for post entry validation of the postcode to occur so clicking on the form does
            stepone.Click();

            IList<IWebElement> valid = wait.Until(d => d.FindElements(By.ClassName("icon--valid")));

            Assert.AreEqual(valid[0].Displayed, true);
            IWebElement houseNumber = wait.Until(d => d.FindElement(By.Name("default_house_number")));
            Assert.AreEqual(houseNumber.Displayed, true);
            houseNumber.SendKeys("201");

            // not perfect but sendkeys doesnt allow for post entry validation of the housenumber to occur so clicking on the form does
            stepone.Click();

            valid = wait.Until(d => d.FindElements(By.ClassName("icon--valid")));

            //Two should be visible on the page
            Assert.AreEqual(valid[1].Displayed, true);

            IWebElement next = wait.Until(d => d.FindElement(By.Id("postInfoAndAddress")));
            next.Click();

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            IWebElement steptwo = wait.Until(d => d.FindElement(By.ClassName("booking-calendar")));
                       
            //intermittently fails, timing issue, need to improve wait to handle minor timing issues
            Assert.AreEqual(steptwo.Displayed, true);

            IWebElement bookBtn = wait.Until(d => d.FindElement(By.Id("postFirstBooking")));
                        
            Assert.AreEqual(bookBtn.Displayed, true);
            bookBtn.Click();

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            IWebElement stepthree = wait.Until(d => d.FindElement(By.Id("v3")));
            Assert.AreEqual(stepthree.Displayed, true);

            IWebElement fullName = wait.Until(d => d.FindElement(By.Name("full_name")));
            Assert.AreEqual(fullName.Displayed, true);
            fullName.SendKeys("Peter Mitcham");

            IWebElement telephone = wait.Until(d => d.FindElement(By.Name("telephone")));
            Assert.AreEqual(telephone.Displayed, true);
            telephone.SendKeys("07766742694");

            IWebElement email = wait.Until(d => d.FindElement(By.Name("email")));
            Assert.AreEqual(email.Displayed, true);
            email.SendKeys("mitchamp@aol.com");

            IWebElement confirm = wait.Until(d => d.FindElement(By.ClassName("btn valuation-btn v3-confirm")));
            Assert.AreEqual(confirm.Displayed, true);
            confirm.Click();

        }
    }
}
