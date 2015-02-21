namespace Floreview.Migrations
{
    using Floreview.DataAccess.Context;
    using Floreview.DataAccess.Repositories;
    using Floreview.Models;
    using Floreview.Utils;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FlowerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private void CreateUsers()
        {
            FlowerContext context = new FlowerContext();
            var idManager = new IdentityManagerRepository(context);

            idManager.CreateRole("Admin", "Global Access");
            var adminUser = new ApplicationUser()
            {
                UserName = "mammoetsaus",
                FirstName = "Thomas",
                LastName = "Voet",
                Email = "thomas.voet@live.be",
                AccessCode = CodeEngine.GenerateRandomCode(4)
            };
            idManager.Create(adminUser, "P@ssw0rd");
            idManager.AddUserToRole(adminUser.Id, "Admin");


            idManager.CreateRole("Admin", "Global Access");
            var secondAdminUser = new ApplicationUser()
            {
                UserName = "dipsaus",
                FirstName = "Juan",
                LastName = "Garcia",
                Email = "juan.garcia@live.be",
                AccessCode = CodeEngine.GenerateRandomCode(4)
            };
            idManager.Create(secondAdminUser, "P@ssw0rd");
            idManager.AddUserToRole(secondAdminUser.Id, "Admin");
        }

        private void CreateProvinces()
        {
            FlowerContext context = new FlowerContext();

            Province p1 = new Province() { Name = "West-Vlaanderen" };
            Province p2 = new Province() { Name = "Oost-Vlaanderen" };
            Province p3 = new Province() { Name = "Antwerpen" };
            Province p4 = new Province() { Name = "Limburg" };
            Province p5 = new Province() { Name = "Vlaams Brabant" };
            Province p6 = new Province() { Name = "Brussels Hoofdstedelijk gewest" };
            Province p7 = new Province() { Name = "Henegouwen" };
            Province p8 = new Province() { Name = "Namen" };
            Province p9 = new Province() { Name = "Luik" };
            Province p10 = new Province() { Name = "Luxemburg" };
            Province p11 = new Province() { Name = "Waals Brabant" };

            context.Province.Add(p1);
            context.Province.Add(p2);
            context.Province.Add(p3);
            context.Province.Add(p4);
            context.Province.Add(p5);
            context.Province.Add(p6);
            context.Province.Add(p7);
            context.Province.Add(p8);
            context.Province.Add(p9);
            context.Province.Add(p10);
            context.Province.Add(p11);
            context.SaveChanges();
        }

        private void CreateLocations()
        {
            FlowerContext context = new FlowerContext();

            String path = AppDomain.CurrentDomain.BaseDirectory + "locations.json";
            StreamReader reader = new StreamReader(path);
            String j = reader.ReadToEnd();

            List<LocationParse> locations = JsonConvert.DeserializeObject<List<LocationParse>>(j);
            List<Province> listProvinces = context.Province.ToList<Province>();

            foreach (LocationParse l in locations)
            {
                Location parsedLocation = new Location()
                {
                    Zip = l.Zip,
                    City = l.City,
                    ZipMain = l.ZipMain,
                    CityMain = l.CityMain,
                    Province = listProvinces.First(i => i.Name.Equals(l.Province))
                };

                context.Location.Add(parsedLocation);
            }

            context.SaveChanges();
        }

        private void CreateCompanies()
        {
            FlowerContext context = new FlowerContext();

            Company c1 = new Company()
            {
                Name = "Thomas' bloemenwinkel",
                Address = "Boombosstraat 71",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.920891 51.036445)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_image_0;profile_1_image_1;profile_1_image_2;profile_1_image_3",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.voet@live.be",
                Facebook = "voetthomas",
                Florist = new Florist()
                {
                    FirstName = "Thomas",
                    LastName = "Voet",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_1_florist.jpg"
                },
                Location = context.Location.First(i => i.City.Equals("Kalken")),
                Genre = new Genre()
                {
                    Name = "Hedendaags"
                }
            };

            Company c2 = new Company()
            {
                Name = "Zotte bloemen",
                Address = "Lange violettestraat 15",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.734070 51.048552)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.voet@student.howest.be",
                Facebook = "brecht.gemmel",
                Florist = new Florist()
                {
                    FirstName = "Jan",
                    LastName = "Met de pet",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/4"
                },
                Location = context.Location.First(i => i.City.Equals("Gent")),
                Genre = new Genre()
                {
                    Name = "Ouderwets"
                }
            };

            Company c3 = new Company()
            {
                Name = "Zwarte knalpotten",
                Address = "Kerkstraat 2",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.855433 51.036439)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.mammoetsaus@gmail.be",
                Facebook = "kaspar.chabot",
                Florist = new Florist()
                {
                    FirstName = "Koen",
                    LastName = "Is een nicht",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/4"
                },
                Location = context.Location.First(i => i.City.Equals("Laarne")),
                Genre = new Genre()
                {
                    Name = "Experimenteel"
                }
            };

            Company c4 = new Company()
            {
                Name = "Maria's bloemen",
                Address = "Elfnovemberstraat 7",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.679745 51.072267)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.voet@live.be",
                Facebook = "gilles.carron.3",
                Florist = new Florist()
                {
                    FirstName = "Gilles",
                    LastName = "Carron",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/4"
                },
                Location = context.Location.First(i => i.City.Equals("Mariakerke")),
                Genre = new Genre()
                {
                    Name = "Origineel"
                }
            };

            Company c5 = new Company()
            {
                Name = "Bloemen Kevin",
                Address = "Wachtebekestraat 15",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.816564 51.200252)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.voet@student.howest.be",
                Facebook = "louis.vormezeele",
                Florist = new Florist()
                {
                    FirstName = "Louis",
                    LastName = "Vormezeele",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/4"
                },
                Location = context.Location.First(i => i.City.Equals("Zelzate")),
                Genre = new Genre()
                {
                    Name = "Modern"
                }
            };

            Company c6 = new Company()
            {
                Name = "Kalkense bloemen",
                Address = "Vaartstraat 24",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.915618 51.035375)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "laurens.voet@live.be",
                Facebook = "voetthomas",
                Florist = new Florist()
                {
                    FirstName = "Laurens",
                    LastName = "Voet",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/2"
                },
                Location = context.Location.First(i => i.City.Equals("Kalken")),
                Genre = new Genre()
                {
                    Name = "Hedendaags"
                }
            };

            Company c7 = new Company()
            {
                Name = "Bloemen van Eeklo",
                Address = "Eeklostraat 15",
                DescriptionShortNL = "NL - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongNL = "NL - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortEN = "EN - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongEN = "EN - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortFR = "FR - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongFR = "FR - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                DescriptionShortDE = "DE - Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater.",
                DescriptionLongDE = "DE - Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.743870 51.178863)"),
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "laurens.voet@live.be",
                Facebook = "voetthomas",
                Florist = new Florist()
                {
                    FirstName = "Laurens",
                    LastName = "Voet",
                    Phone = "+3293457673",
                    Cellphone = "+32499434095",
                    ImagePath = "http://images.com/2"
                },
                Location = context.Location.First(i => i.City.Equals("Kaprijke")),
                Genre = new Genre()
                {
                    Name = "Hedendaags"
                }
            };

            context.Company.Add(c1);
            context.Company.Add(c2);
            context.Company.Add(c3);
            context.Company.Add(c4);
            context.Company.Add(c5);
            context.Company.Add(c6);
            context.Company.Add(c7);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        private void CreateBlogCategories()
        {
            FlowerContext context = new FlowerContext();

            BlogCategory b1 = new BlogCategory()
            {
                Name = "DIY"
            };

            BlogCategory b2 = new BlogCategory()
            {
                Name = "Quick read"
            };

            BlogCategory b3 = new BlogCategory()
            {
                Name = "Story"
            };

            context.BlogCategory.Add(b1);
            context.BlogCategory.Add(b2);
            context.BlogCategory.Add(b3);
            context.SaveChanges();
        }

        private void CreateBlogElements()
        {
            FlowerContext context = new FlowerContext();

            BlogElement b1 = new BlogElement()
            {
                Name = "Paragraaf",
                Layout = "<h2>Paragraaf</h2> <ul> <li class=\"blog-element-min\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <rect x=\"8\" y=\"29\" fill=\"#333333\" width=\"24\" height=\"3\"/> </svg> </li><li class=\"blog-element-max\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <rect x=\"10\" y=\"6\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"/> <rect x=\"6\" y=\"10\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"/> </g> </svg> </li><li class=\"blog-element-del\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <path fill=\"#333333\" d=\"M32,10.4L29.6,8L20,17.6L10.4,8L8,10.4l9.6,9.6L8,29.6l2.4,2.4l9.6-9.6l9.6,9.6l2.4-2.4L22.4,20L32,10.4z M32,10.4\"/> </g> </svg> </li></ul> <textarea class=\"blog-element-plain-text-nl\" placeholder=\"Inhoud (NL)\"></textarea> <textarea class=\"blog-element-plain-text-en\" placeholder=\"Inhoud (EN)\"></textarea> <textarea class=\"blog-element-plain-text-fr\" placeholder=\"Inhoud (FR)\"></textarea> <textarea class=\"blog-element-plain-text-de\" placeholder=\"Inhoud (DE)\"></textarea>",
                SVG = "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"\r\n\t viewBox=\"0 0 16 16\" enable-background=\"new 0 0 16 16\" xml:space=\"preserve\">\r\n<g>\r\n\t<path fill=\"#333333\" d=\"M14,0H2C1.448,0,1,0.448,1,1v14c0,0.552,0.448,1,1,1h12c0.552,0,1-0.448,1-1V1C15,0.448,14.552,0,14,0z M14,15H2V1h12V15z\"/>\r\n\t<path fill=\"#333333\" d=\"M7.5,4h4C11.775,4,12,3.776,12,3.5S11.775,3,11.5,3h-4C7.224,3,7,3.224,7,3.5S7.224,4,7.5,4z\"/>\r\n\t<path fill=\"#333333\" d=\"M4.5,7h7C11.775,7,12,6.776,12,6.5S11.775,6,11.5,6h-7C4.224,6,4,6.224,4,6.5S4.224,7,4.5,7z\"/>\r\n\t<path fill=\"#333333\" d=\"M4.5,10h7c0.275,0,0.5-0.225,0.5-0.5S11.775,9,11.5,9h-7C4.224,9,4,9.225,4,9.5S4.224,10,4.5,10z\"/>\r\n\t<path fill=\"#333333\" d=\"M4.5,13h7c0.275,0,0.5-0.225,0.5-0.5S11.775,12,11.5,12h-7C4.224,12,4,12.225,4,12.5S4.224,13,4.5,13z\"/>\r\n</g>\r\n</svg>\r\n"
            };

            BlogElement b2 = new BlogElement()
            {
                Name = "Opsomming",
                Layout = "<h2>Opsomming</h2> <ul> <li class=\"blog-element-min\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <rect x=\"8\" y=\"29\" fill=\"#333333\" width=\"24\" height=\"3\"></rect> </svg> </li><li class=\"blog-element-max\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <rect x=\"10\" y=\"6\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"></rect> <rect x=\"6\" y=\"10\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"></rect> </g> </svg> </li><li class=\"blog-element-del\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <path fill=\"#333333\" d=\"M32,10.4L29.6,8L20,17.6L10.4,8L8,10.4l9.6,9.6L8,29.6l2.4,2.4l9.6-9.6l9.6,9.6l2.4-2.4L22.4,20L32,10.4z M32,10.4\"></path> </g> </svg> </li></ul> <input type=\"text\" placeholder=\"Hoofding (NL)\" class=\"blog-element-enumeration-nl\"/> <input type=\"text\" placeholder=\"Hoofding (EN)\" class=\"blog-element-enumeration-en\"/> <input type=\"text\" placeholder=\"Hoofding (FR)\" class=\"blog-element-enumeration-fr\"/> <input type=\"text\" placeholder=\"Hoofding (DE)\" class=\"blog-element-enumeration-de\"/>",
                SVG = "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"\r\n\t viewBox=\"0 0 64 64\" enable-background=\"new 0 0 64 64\" xml:space=\"preserve\">\r\n<g>\r\n\t<path fill=\"#333333\" d=\"M11.374,5.317c-2.945,0-5.342,2.397-5.342,5.344s2.396,5.343,5.342,5.343c2.946,0,5.343-2.396,5.343-5.343\r\n\t\tS14.32,5.317,11.374,5.317z M11.374,14.401c-2.063,0-3.741-1.678-3.741-3.74c0-2.063,1.678-3.743,3.741-3.743\r\n\t\tc2.064,0,3.742,1.68,3.742,3.743C15.116,12.724,13.438,14.401,11.374,14.401z\"/>\r\n\t<path fill=\"#333333\" d=\"M11.374,26.445c-2.945,0-5.342,2.396-5.342,5.343c0,2.944,2.396,5.34,5.342,5.34\r\n\t\tc2.946,0,5.343-2.396,5.343-5.34C16.717,28.842,14.32,26.445,11.374,26.445z M11.374,35.526c-2.063,0-3.741-1.677-3.741-3.738\r\n\t\tc0-2.063,1.678-3.74,3.741-3.74c2.064,0,3.742,1.678,3.742,3.74C15.116,33.85,13.438,35.526,11.374,35.526z\"/>\r\n\t<path fill=\"#333333\" d=\"M11.374,47.997c-2.945,0-5.342,2.396-5.342,5.343s2.396,5.343,5.342,5.343c2.946,0,5.343-2.396,5.343-5.343\r\n\t\tS14.32,47.997,11.374,47.997z M11.374,57.081c-2.063,0-3.741-1.68-3.741-3.741c0-2.063,1.678-3.742,3.741-3.742\r\n\t\tc2.064,0,3.742,1.68,3.742,3.742C15.116,55.401,13.438,57.081,11.374,57.081z\"/>\r\n\t<rect x=\"25.755\" y=\"9.766\" fill=\"#333333\" width=\"32.213\" height=\"1.79\"/>\r\n\t<rect x=\"25.755\" y=\"31.738\" fill=\"#333333\" width=\"32.213\" height=\"1.79\"/>\r\n\t<rect x=\"25.755\" y=\"52.131\" fill=\"#333333\" width=\"32.213\" height=\"1.789\"/>\r\n</g>\r\n</svg>"
            };

            BlogElement b3 = new BlogElement()
            {
                Name = "Afbeelding",
                Layout = "<h2>Afbeelding</h2> <ul> <li class=\"blog-element-min\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <rect x=\"8\" y=\"29\" fill=\"#333333\" width=\"24\" height=\"3\"></rect> </svg> </li><li class=\"blog-element-max\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <rect x=\"10\" y=\"6\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"></rect> <rect x=\"6\" y=\"10\" fill=\"#FFFFFF\" stroke=\"#333333\" stroke-width=\"2\" stroke-miterlimit=\"10\" width=\"24\" height=\"24\"></rect> </g> </svg> </li><li class=\"blog-element-del\"> <svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 40 40\" enable-background=\"new 0 0 40 40\" xml:space=\"preserve\"> <g> <path fill=\"#333333\" d=\"M32,10.4L29.6,8L20,17.6L10.4,8L8,10.4l9.6,9.6L8,29.6l2.4,2.4l9.6-9.6l9.6,9.6l2.4-2.4L22.4,20L32,10.4z M32,10.4\"></path> </g> </svg> </li></ul> <input type=\"file\" accept=\"image/*\"/>",
                SVG = "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 512 512\" enable-background=\"new 0 0 512 512\" xml:space=\"preserve\"> <path fill=\"#333333\" d=\"M0,64v416h512V64H0z M480,448H32V96h448V448z M352,176c0,26.51,21.49,48,48,48s48-21.49,48-48s-21.49-48-48-48 S352,149.49,352,176z M448,416H64l96-256l128,160l64-48L448,416z\"/> </svg>"
            };

            context.BlogElement.Add(b1);
            context.BlogElement.Add(b2);
            context.BlogElement.Add(b3);
            context.SaveChanges();
        }


        protected override void Seed(FlowerContext context)
        {
            CreateUsers();

            CreateProvinces();

            CreateLocations();

            CreateBlogElements();
        }
    }

    public class LocationParse
    {
        public int Zip { get; set; }

        public String City { get; set; }

        public int ZipMain { get; set; }

        public String CityMain { get; set; }

        public String Province { get; set; }
    }
}