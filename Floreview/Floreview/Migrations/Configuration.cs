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
            CodeEngine codeEngine = new CodeEngine();

            idManager.CreateRole("Admin", "Global Access");
            var adminUser = new ApplicationUser()
            {
                UserName = "mammoetsaus",
                FirstName = "Thomas",
                LastName = "Voet",
                Email = "thomas.voet@live.be",
                AccessCode = codeEngine.GenerateRandomCode()
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
                AccessCode = codeEngine.GenerateRandomCode()
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
                DescriptionShort = "Leggings hella twee, tote bag meggings quinoa pour-over Godard High Life viral Marfa messenger bag kogi. Sriracha pop-up Cosby sweater synth Schlitz.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
                Coordinates = DbGeography.FromText("POINT(3.920891 51.036445)"),        // longitude - latitude
                Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg",
                ImageList = "profile_1_1;profile_1_2;profile_1_3;profile_1_4;profile_1_5",
                Website = "http://mammoetsaus.azurewebsites.net",
                Email = "thomas.voet@live.be",
                Facebook = "voetthomas",
                Florist = new Florist()
                {
                    FirstName = "Thomas",
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

            Company c2 = new Company()
            {
                Name = "Zotte bloemen",
                Address = "Lange violettestraat 15",
                DescriptionShort = "Keytar chia Thundercats Marfa irony brunch lo-fi, Odd Future authentic art party actually pork belly tofu. PBR fashion axe PBR&B beard pickled.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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
                DescriptionShort = "Meggings keffiyeh Godard DIY, beard gluten-free Tumblr. Crucifix normcore seitan, salvia chambray VHS disrupt organic cliche flexitarian. Mustache.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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
                DescriptionShort = "Kickstarter dreamcatcher irony pickled Banksy. Fingerstache bespoke umami stumptown. Mixtape tattooed chillwave ugh cardigan fixie.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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
                DescriptionShort = "Shabby chic mumblecore VHS keffiyeh vinyl tousled. Umami art party mumblecore pour-over. Cornhole single-origin coffee Brooklyn, freegan.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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
                DescriptionShort = "Cliche Pinterest meggings yr forage Kickstarter. PBR&B beard quinoa fingerstache authentic. Portland church-key small batch crucifix.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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
                DescriptionShort = "Cliche Pinterest meggings yr forage Kickstarter. PBR&B beard quinoa fingerstache authentic. Portland church-key small batch crucifix.",
                DescriptionLong = "Flexitarian Godard deep v freegan beard literally. Jean shorts quinoa biodiesel yr. Bushwick YOLO chia sriracha, disrupt messenger bag Intelligentsia four loko leggings Etsy. Hella keffiyeh locavore XOXO, bitters authentic brunch distillery narwhal kitsch fap biodiesel pickled Wes Anderson. Asymmetrical semiotics pickled master cleanse, raw denim plaid pop-up vegan Truffaut Etsy actually Thundercats cray. Chillwave single-origin coffee PBR, direct trade brunch farm-to-table 3 wolf moon deep v.",
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

        private void CreateBlogTypes()
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

        private void CreateBlogItems()
        {
            FlowerContext context = new FlowerContext();
            var idManager = new IdentityManagerRepository(context);

            Blog b1 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "How to train a dragon?",
                Timestamp = new DateTime(2014, 8, 1, 0, 0, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("DIY")),
                Author = idManager.FindAsync("mammoetsaus", "P@ssw0rd")
            };

            Blog b2 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "Water found on Mars. Wait what?!",
                Timestamp = new DateTime(2014, 9, 1, 0, 0, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("Quick read")),
                Author = idManager.FindAsync("mammoetsaus", "P@ssw0rd")
            };

            Blog b3 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "I visited my grandmother yesterday.",
                Timestamp = new DateTime(2013, 5, 1, 0, 0, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("Story")),
                Author = idManager.FindAsync("mammoetsaus", "P@ssw0rd")
            };

            Blog b4 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "We spent the afternoon chasing pavements.",
                Timestamp = new DateTime(2014, 9, 1, 0, 0, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("Story")),
                Author = idManager.FindAsync("mammoetsaus", "P@ssw0rd")
            };

            Blog b5 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "How to create your own space station.",
                Timestamp = new DateTime(2014, 9, 7, 15, 0, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("DIY")),
                Author = idManager.FindAsync("mammoetsaus", "P@ssw0rd")
            };

            Blog b6 = new Blog()
            {
                Avatar = "http://floreview.blob.core.windows.net/blog/news_item.jpg",
                Title = "How to cross the border?",
                Timestamp = new DateTime(2014, 9, 7, 12, 45, 0),
                Category = context.BlogCategory.First(i => i.Name.Equals("DIY")),
                Author = idManager.FindAsync("dipsaus", "P@ssw0rd")
            };

            context.Blog.Add(b1);
            context.Blog.Add(b2);
            context.Blog.Add(b3);
            context.Blog.Add(b4);
            context.Blog.Add(b5);
            context.Blog.Add(b6);
            context.SaveChanges();
        }


        protected override void Seed(FlowerContext context)
        {
            CreateUsers();

            CreateProvinces();

            CreateLocations();

            CreateCompanies();

            CreateBlogTypes();

            CreateBlogItems();
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