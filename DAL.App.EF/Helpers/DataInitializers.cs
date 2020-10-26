using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }


        public static void SeedData(AppDbContext context)
        {
            var productTypes = new ProductType[]
            {
                new ProductType()
                {
                    Name = "Maal",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new ProductType()
                {
                    Name = "Kaart",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new ProductType()
                {
                    Name = "Paelad ja lipsud",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new ProductType()
                {
                    Name = "Kutse",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                },
                new ProductType()
                {
                    Name = "Kingikarp",
                    Id = new Guid("00000000-0000-0000-0000-000000000005")
                },
                new ProductType()
                {
                    Name = "Dekoratsioon",
                    Id = new Guid("00000000-0000-0000-0000-000000000006")
                },
            };

            foreach (var productType in productTypes)
            {
                if (!context.ProductTypes.Any(l => l.Id == productType.Id))
                {
                    context.ProductTypes.Add(productType);
                }
            }

            context.SaveChanges();

            var paymentTypes = new PaymentType[]
            {
                new PaymentType()
                {
                    Name = "Swedbank",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new PaymentType()
                {
                    Name = "SEB",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new PaymentType()
                {
                    Name = "LHV pank",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new PaymentType()
                {
                    Name = "Luminor Nordea",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                }
            };

            foreach (var paymentType in paymentTypes)
            {
                if (!context.PaymentTypes.Any(l => l.Id == paymentType.Id))
                {
                    context.PaymentTypes.Add(paymentType);
                }
            }

            context.SaveChanges();

            var destinations = new Destination[]
            {
                new Destination()
                {
                    Location = "Ahtme Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000000")
                },

                new Destination()
                {
                    Location = "Antsla Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000001")
                },

                new Destination()
                {
                    Location = "Aravete Meie kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000002")
                },

                new Destination()
                {
                    Location = "Aruküla Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000003")
                },

                new Destination()
                {
                    Location = "Aseri Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000004")
                },

                new Destination()
                {
                    Location = "Audru osavallakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000005")
                },

                new Destination()
                {
                    Location = "Elva Arbimäe Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000006")
                },

                new Destination()
                {
                    Location = "Elva Turuplatsi Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000007")
                },

                new Destination()
                {
                    Location = "Haabneeme Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000008")
                },

                new Destination()
                {
                    Location = "Haapsalu Kastani pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000009")
                },

                new Destination()
                {
                    Location = "Haapsalu Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000010")
                },

                new Destination()
                {
                    Location = "Haapsalu Uuemõisa Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000011")
                },

                new Destination()
                {
                    Location = "Haljala Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000012")
                },

                new Destination()
                {
                    Location = "Häädemeeste Coop pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000013")
                },

                new Destination()
                {
                    Location = "Ihaste Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000014")
                },

                new Destination()
                {
                    Location = "Iisaku Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000015")
                },

                new Destination()
                {
                    Location = "Jõgeva Postkontori pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000016")
                },

                new Destination()
                {
                    Location = "Jõhvi Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000017")
                },

                new Destination()
                {
                    Location = "Jõhvi Jewe Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000018")
                },

                new Destination()
                {
                    Location = "Jõhvi Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000019")
                },

                new Destination()
                {
                    Location = "Jõhvi Pargi Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000020")
                },

                new Destination()
                {
                    Location = "Järva-Jaani Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000021")
                },

                new Destination()
                {
                    Location = "Järvakandi Kandi Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000022")
                },

                new Destination()
                {
                    Location = "Jüri Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000023")
                },

                new Destination()
                {
                    Location = "Jüri Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000024")
                },

                new Destination()
                {
                    Location = "Kadrina Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000025")
                },

                new Destination()
                {
                    Location = "Karksi-Nuia pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000026")
                },

                new Destination()
                {
                    Location = "Kehra pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000027")
                },

                new Destination()
                {
                    Location = "Kehtna A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000028")
                },

                new Destination()
                {
                    Location = "Keila Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000029")
                },

                new Destination()
                {
                    Location = "Keila Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000030")
                },

                new Destination()
                {
                    Location = "Keila Rõõmu kaubamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000031")
                },

                new Destination()
                {
                    Location = "Kiili Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000032")
                },

                new Destination()
                {
                    Location = "Kiisa A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000033")
                },

                new Destination()
                {
                    Location = "Kilingi-Nõmme Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000034")
                },

                new Destination()
                {
                    Location = "Kiviõli Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000035")
                },

                new Destination()
                {
                    Location = "Kiviõli Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000036")
                },

                new Destination()
                {
                    Location = "Koeru Vallamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000037")
                },

                new Destination()
                {
                    Location = "Kohila Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000038")
                },

                new Destination()
                {
                    Location = "Kohtla-Järve Ahtme Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000039")
                },

                new Destination()
                {
                    Location = "Kohtla-Järve Keskallee Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000040")
                },

                new Destination()
                {
                    Location = "Kohtla-Järve Mõisa tee Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000041")
                },

                new Destination()
                {
                    Location = "Kohtla-Järve Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000042")
                },

                new Destination()
                {
                    Location = "Kohtla-Järve Vironia Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000043")
                },

                new Destination()
                {
                    Location = "Kose Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000044")
                },

                new Destination()
                {
                    Location = "Kostivere Meie kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000045")
                },

                new Destination()
                {
                    Location = "Kunda Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000046")
                },

                new Destination()
                {
                    Location = "Kuressaare Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000047")
                },

                new Destination()
                {
                    Location = "Kuressaare Smuuli pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000048")
                },

                new Destination()
                {
                    Location = "Kuusalu pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000049")
                },

                new Destination()
                {
                    Location = "Kõrveküla A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000050")
                },

                new Destination()
                {
                    Location = "Käina Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000051")
                },

                new Destination()
                {
                    Location = "Kärdla Hiiu Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000052")
                },

                new Destination()
                {
                    Location = "Kärdla Tormi Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000053")
                },

                new Destination()
                {
                    Location = "Laagri Comarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000054")
                },

                new Destination()
                {
                    Location = "Laagri Maksimarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000055")
                },

                new Destination()
                {
                    Location = "Lagedi Keskusehoone pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000056")
                },

                new Destination()
                {
                    Location = "Lihula Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000057")
                },

                new Destination()
                {
                    Location = "Loksa Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000058")
                },

                new Destination()
                {
                    Location = "Loo Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000059")
                },

                new Destination()
                {
                    Location = "Maardu Maxima X pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000060")
                },

                new Destination()
                {
                    Location = "Maardu Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000061")
                },

                new Destination()
                {
                    Location = "Maardu Pärli Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000062")
                },

                new Destination()
                {
                    Location = "Muhu Liiva Alexela pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000063")
                },

                new Destination()
                {
                    Location = "Muraste Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000064")
                },

                new Destination()
                {
                    Location = "Mustvee Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000065")
                },

                new Destination()
                {
                    Location = "Muuga Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000066")
                },

                new Destination()
                {
                    Location = "Märjamaa Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000067")
                },

                new Destination()
                {
                    Location = "Narva Astri keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000068")
                },

                new Destination()
                {
                    Location = "Narva Fama Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000069")
                },

                new Destination()
                {
                    Location = "Narva Kerese keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000070")
                },

                new Destination()
                {
                    Location = "Narva Kreenholmi Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000071")
                },

                new Destination()
                {
                    Location = "Narva Megamarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000072")
                },

                new Destination()
                {
                    Location = "Narva Prisma pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000073")
                },

                new Destination()
                {
                    Location = "Narva Tiimani Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000074")
                },

                new Destination()
                {
                    Location = "Narva-Jõesuu raamatukogu pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000075")
                },

                new Destination()
                {
                    Location = "Nõo vallamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000076")
                },

                new Destination()
                {
                    Location = "Orissaare pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000077")
                },

                new Destination()
                {
                    Location = "Otepää Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000078")
                },

                new Destination()
                {
                    Location = "Paide Hesburgeri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000079")
                },

                new Destination()
                {
                    Location = "Paide Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000080")
                },

                new Destination()
                {
                    Location = "Paikuse Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000081")
                },

                new Destination()
                {
                    Location = "Paldiski Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000082")
                },

                new Destination()
                {
                    Location = "Paldiski Maxima X pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000083")
                },

                new Destination()
                {
                    Location = "Peetri Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000084")
                },

                new Destination()
                {
                    Location = "Peetri Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000085")
                },

                new Destination()
                {
                    Location = "Puhja Coop kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000086")
                },

                new Destination()
                {
                    Location = "Põltsamaa Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000087")
                },

                new Destination()
                {
                    Location = "Põlva Kaubamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000088")
                },

                new Destination()
                {
                    Location = "Põlva Meie kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000089")
                },

                new Destination()
                {
                    Location = "Põlva Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000090")
                },

                new Destination()
                {
                    Location = "Pärnu Jannseni Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000091")
                },

                new Destination()
                {
                    Location = "Pärnu Kaubamajaka pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000092")
                },

                new Destination()
                {
                    Location = "Pärnu Oja Comarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000093")
                },

                new Destination()
                {
                    Location = "Pärnu Maksimarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000094")
                },

                new Destination()
                {
                    Location = "Pärnu Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000095")
                },

                new Destination()
                {
                    Location = "Pärnu Port Artur 2 pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000096")
                },

                new Destination()
                {
                    Location = "Pärnu Tiina Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000097")
                },

                new Destination()
                {
                    Location = "Pärnu Turu pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000098")
                },

                new Destination()
                {
                    Location = "Pärnu Ülejõe Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000099")
                },

                new Destination()
                {
                    Location = "Pärnu-Jaagupi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000100")
                },

                new Destination()
                {
                    Location = "Raasiku pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000101")
                },

                new Destination()
                {
                    Location = "Rakvere Lilleoru kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000102")
                },

                new Destination()
                {
                    Location = "Rakvere Põhjakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000103")
                },

                new Destination()
                {
                    Location = "Rakvere Turutare pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000104")
                },

                new Destination()
                {
                    Location = "Rakvere Vaala keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000105")
                },

                new Destination()
                {
                    Location = "Rapla Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000106")
                },

                new Destination()
                {
                    Location = "Rapla Vestis Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000107")
                },

                new Destination()
                {
                    Location = "Risti vallamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000108")
                },

                new Destination()
                {
                    Location = "Rõngu Coop kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000109")
                },

                new Destination()
                {
                    Location = "Räpina Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000110")
                },

                new Destination()
                {
                    Location = "Räpina Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000111")
                },

                new Destination()
                {
                    Location = "Saaremaa Kaubamaja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000112")
                },

                new Destination()
                {
                    Location = "Salme Rannapargi tee pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000113")
                },

                new Destination()
                {
                    Location = "Saku Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000114")
                },

                new Destination()
                {
                    Location = "Saku Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000115")
                },

                new Destination()
                {
                    Location = "Saue Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000116")
                },

                new Destination()
                {
                    Location = "Saue Kaubakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000117")
                },

                new Destination()
                {
                    Location = "Sauga Coop kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000118")
                },

                new Destination()
                {
                    Location = "Suure-Jaani pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000119")
                },

                new Destination()
                {
                    Location = "Sillamäe Maksimarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000120")
                },

                new Destination()
                {
                    Location = "Sillamäe Viru pst Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000121")
                },

                new Destination()
                {
                    Location = "Sindi Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000122")
                },

                new Destination()
                {
                    Location = "Tabasalu Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000123")
                },

                new Destination()
                {
                    Location = "Tabivere Saadjärve A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000124")
                },

                new Destination()
                {
                    Location = "Tallinna Akadeemia Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000125")
                },

                new Destination()
                {
                    Location = "Tallinna Arsenali keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000126")
                },

                new Destination()
                {
                    Location = "Tallinna Balti Jaama pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000127")
                },

                new Destination()
                {
                    Location = "Tallinna Coca-Cola Plaza pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000128")
                },

                new Destination()
                {
                    Location = "Tallinna Ehitajate tee Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000129")
                },

                new Destination()
                {
                    Location = "Tallinna Ehitajate tee Maxima XXX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000130")
                },

                new Destination()
                {
                    Location = "Tallinna Haabersti Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000131")
                },

                new Destination()
                {
                    Location = "Tallinna Idakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000132")
                },

                new Destination()
                {
                    Location = "Tallinna Juhkentali Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000133")
                },

                new Destination()
                {
                    Location = "Tallinna Järve Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000134")
                },

                new Destination()
                {
                    Location = "Tallinna Järveotsa Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000135")
                },

                new Destination()
                {
                    Location = "Tallinna Kadaka Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000136")
                },

                new Destination()
                {
                    Location = "Tallinna Kakumäe Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000137")
                },

                new Destination()
                {
                    Location = "Tallinna Kari Grossipoe pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000138")
                },

                new Destination()
                {
                    Location = "Tallinna Kivila Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000139")
                },

                new Destination()
                {
                    Location = "Tallinna Kolde Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000140")
                },

                new Destination()
                {
                    Location = "Tallinna Kollase Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000141")
                },

                new Destination()
                {
                    Location = "Tallinna Kristiine Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000142")
                },

                new Destination()
                {
                    Location = "Tallinna Kärberi Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000143")
                },

                new Destination()
                {
                    Location = "Tallinna Lasnamäe Centrumi A pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000144")
                },

                new Destination()
                {
                    Location = "Tallinna Lasnamäe Centrumi B pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000145")
                },

                new Destination()
                {
                    Location = "Tallinna Lasnamäe Prisma pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000146")
                },

                new Destination()
                {
                    Location = "Tallinna Lastekodu Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000147")
                },

                new Destination()
                {
                    Location = "Tallinna Lauluväljaku Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000148")
                },

                new Destination()
                {
                    Location = "Tallinna Linnamäe Maxima XXX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000149")
                },

                new Destination()
                {
                    Location = "Tallinna Läänemere Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000150")
                },

                new Destination()
                {
                    Location = "Tallinna Läänemere Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000151")
                },

                new Destination()
                {
                    Location = "Tallinna Magdaleena pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000152")
                },

                new Destination()
                {
                    Location = "Tallinna Magistrali Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000153")
                },

                new Destination()
                {
                    Location = "Tallinna Marienthali Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000154")
                },

                new Destination()
                {
                    Location = "Tallinna Merimetsa Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000155")
                },

                new Destination()
                {
                    Location = "Tallinna Mustakivi Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000156")
                },

                new Destination()
                {
                    Location = "Tallinna Mustamäe Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000157")
                },

                new Destination()
                {
                    Location = "Tallinna Mustika Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000158")
                },

                new Destination()
                {
                    Location = "Tallinna Männiku Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000159")
                },

                new Destination()
                {
                    Location = "Tallinna Nautica Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000160")
                },

                new Destination()
                {
                    Location = "Tallinna Nurmenuku pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000161")
                },

                new Destination()
                {
                    Location = "Tallinna Nõmme tee 23 Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000162")
                },

                new Destination()
                {
                    Location = "Tallinna Nõmme turu pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000163")
                },

                new Destination()
                {
                    Location = "Tallinna Paasiku Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000164")
                },

                new Destination()
                {
                    Location = "Tallinna Paepargi Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000165")
                },

                new Destination()
                {
                    Location = "Tallinna Pallasti pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000166")
                },

                new Destination()
                {
                    Location = "Tallinna Pelgulinna Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000167")
                },

                new Destination()
                {
                    Location = "Tallinna Peterburi tee 34/4 pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000168")
                },

                new Destination()
                {
                    Location = "Tallinna Peterburi tee Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000169")
                },

                new Destination()
                {
                    Location = "Tallinna Pirita Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000170")
                },

                new Destination()
                {
                    Location = "Tallinna Pirita Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000171")
                },

                new Destination()
                {
                    Location = "Tallinna Punase Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000172")
                },

                new Destination()
                {
                    Location = "Tallinna Põhja Neste pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000173")
                },

                new Destination()
                {
                    Location = "Tallinna Pärnu mnt 390b Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000174")
                },

                new Destination()
                {
                    Location = "Tallinna Pääsküla Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000175")
                },

                new Destination()
                {
                    Location = "Tallinna Raudalu Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000176")
                },

                new Destination()
                {
                    Location = "Tallinna Rocca al Mare pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000177")
                },

                new Destination()
                {
                    Location = "Tallinna Sikupilli pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000178")
                },

                new Destination()
                {
                    Location = "Tallinna Solaris Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000179")
                },

                new Destination()
                {
                    Location = "Tallinna Stockmanni pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000180")
                },

                new Destination()
                {
                    Location = "Tallinna Stroomi Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000181")
                },

                new Destination()
                {
                    Location = "Tallinna Sõle Rimi (Paavli 10) pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000182")
                },

                new Destination()
                {
                    Location = "Tallinna Sõpruse pst 171 Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000183")
                },

                new Destination()
                {
                    Location = "Tallinna Sõpruse Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000184")
                },

                new Destination()
                {
                    Location = "Tallinna Sütiste Maxima X pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000185")
                },

                new Destination()
                {
                    Location = "Tallinna T1 Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000186")
                },

                new Destination()
                {
                    Location = "Tallinna Tammsaare Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000187")
                },

                new Destination()
                {
                    Location = "Tallinna Tammsaare Ärikeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000188")
                },

                new Destination()
                {
                    Location = "Tallinna Tatari Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000189")
                },

                new Destination()
                {
                    Location = "Tallinna Telliskivi Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000190")
                },

                new Destination()
                {
                    Location = "Tallinna Tondi Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000191")
                },

                new Destination()
                {
                    Location = "Tallinna Tondi Ärikeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000192")
                },

                new Destination()
                {
                    Location = "Tallinna Torupilli Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000193")
                },

                new Destination()
                {
                    Location = "Tallinna Tähesaju Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000194")
                },

                new Destination()
                {
                    Location = "Tallinna Tööstuse Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000195")
                },

                new Destination()
                {
                    Location = "Tallinna Valdeku Comarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000196")
                },

                new Destination()
                {
                    Location = "Tallinna Valdeku Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000197")
                },

                new Destination()
                {
                    Location = "Tallinna Vikerlase Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000198")
                },

                new Destination()
                {
                    Location = "Tallinna Vilde tee Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000199")
                },

                new Destination()
                {
                    Location = "Tallinna Viru bussiterminali pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000200")
                },

                new Destination()
                {
                    Location = "Tallinna Viru Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000201")
                },

                new Destination()
                {
                    Location = "Tallinna Õismäe Maxima XX pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000202")
                },

                new Destination()
                {
                    Location = "Tallinna Ülemiste City Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000203")
                },

                new Destination()
                {
                    Location = "Tallinna Ülemiste Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000204")
                },

                new Destination()
                {
                    Location = "Tallinna Ümera Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000205")
                },

                new Destination()
                {
                    Location = "Tamsalu Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000206")
                },

                new Destination()
                {
                    Location = "Tapa Grossi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000207")
                },

                new Destination()
                {
                    Location = "Tapa Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000208")
                },

                new Destination()
                {
                    Location = "Tartu Aardla Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000209")
                },

                new Destination()
                {
                    Location = "Tartu Anne Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000210")
                },

                new Destination()
                {
                    Location = "Tartu Anne 40 Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000211")
                },

                new Destination()
                {
                    Location = "Tartu Eedeni pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000212")
                },

                new Destination()
                {
                    Location = "Tartu Ilmatsalu Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000213")
                },

                new Destination()
                {
                    Location = "Tartu Jaamamõisa Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000214")
                },

                new Destination()
                {
                    Location = "Tartu Karete Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000215")
                },

                new Destination()
                {
                    Location = "Tartu Kesklinna Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000216")
                },

                new Destination()
                {
                    Location = "Tartu Kivilinna pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000217")
                },

                new Destination()
                {
                    Location = "Tartu Kvartali Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000218")
                },

                new Destination()
                {
                    Location = "Tartu Lembitu Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000219")
                },

                new Destination()
                {
                    Location = "Tartu Lõunakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000220")
                },

                new Destination()
                {
                    Location = "Tartu Raadi Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000221")
                },

                new Destination()
                {
                    Location = "Tartu Rebase Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000222")
                },

                new Destination()
                {
                    Location = "Tartu Ringtee Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000223")
                },

                new Destination()
                {
                    Location = "Tartu Saare Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000224")
                },

                new Destination()
                {
                    Location = "Tartu Sõbrakeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000225")
                },

                new Destination()
                {
                    Location = "Tartu Sõbra Prisma pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000226")
                },

                new Destination()
                {
                    Location = "Tartu Zeppelini pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000227")
                },

                new Destination()
                {
                    Location = "Tartu Tasku Keskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000228")
                },

                new Destination()
                {
                    Location = "Tartu Tuglase Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000229")
                },

                new Destination()
                {
                    Location = "Tartu Ujula Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000230")
                },

                new Destination()
                {
                    Location = "Tartu Vahi Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000231")
                },

                new Destination()
                {
                    Location = "Tartu Veeriku Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000232")
                },

                new Destination()
                {
                    Location = "Torma A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000233")
                },

                new Destination()
                {
                    Location = "Turba kaupluse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000234")
                },

                new Destination()
                {
                    Location = "Tõrva Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000235")
                },

                new Destination()
                {
                    Location = "Tõrvandi Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000236")
                },

                new Destination()
                {
                    Location = "Türi Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000237")
                },

                new Destination()
                {
                    Location = "Valga Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000238")
                },

                new Destination()
                {
                    Location = "Valga Siili Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000239")
                },

                new Destination()
                {
                    Location = "Vastse-Kuuste A ja O pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000240")
                },

                new Destination()
                {
                    Location = "Viimsi Kaubanduskeskuse pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000241")
                },

                new Destination()
                {
                    Location = "Viimsi Pärnamäe Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000242")
                },

                new Destination()
                {
                    Location = "Viimsi Rimi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000243")
                },

                new Destination()
                {
                    Location = "Viljandi Bussijaama pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000244")
                },

                new Destination()
                {
                    Location = "Viljandi Maksimarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000245")
                },

                new Destination()
                {
                    Location = "Viljandi Männimäe Selveri pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000246")
                },

                new Destination()
                {
                    Location = "Viljandi Tallinna 60 Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000247")
                },

                new Destination()
                {
                    Location = "Viljandi Vaksali Maxima X pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000248")
                },

                new Destination()
                {
                    Location = "Vinni Spordikompleksi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000249")
                },

                new Destination()
                {
                    Location = "Võru kesklinna Circle K pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000250")
                },

                new Destination()
                {
                    Location = "Võru Maksimarketi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000251")
                },

                new Destination()
                {
                    Location = "Võru Maxima pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000252")
                },

                new Destination()
                {
                    Location = "Väike-Maarja pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000253")
                },

                new Destination()
                {
                    Location = "Vändra Konsumi pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000254")
                },

                new Destination()
                {
                    Location = "Vääna-Jõesuu kauplus Maksis pakiautomaat",
                    Id = new Guid("00000000-0000-0000-0000-100000000255")
                },

                new Destination()
                {
                    Location = "Ülenurme Konsumi pakiautomaa",
                    Id = new Guid("00000000-0000-0000-0000-100000000256")
                }
            };

            foreach (var destination in destinations)
            {
                if (!context.Destinations.Any(l => l.Id == destination.Id))
                {
                    context.Destinations.Add(destination);
                }
            }

            context.SaveChanges();
            
            var currencies = new Currency[]
            {
                new Currency()
                {
                    Name = "Euro",
                    Abbreviation = "EUR",
                    Symbol = "€",
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                },
                
                new Currency()
                {
                    Name = "Dollar",
                    Abbreviation = "USD",
                    Symbol = "$",
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                },
                
                new Currency()
                {
                    Name = "Pound",
                    Abbreviation = "GBP",
                    Symbol = "£",
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                }
            };

            foreach (var currency in currencies)
            {
                if (!context.Currencies.Any(l => l.Id == currency.Id))
                {
                    context.Currencies.Add(currency);
                }
            }

            context.SaveChanges();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }


            var users = new (string name, string password, string firstName, string lastName, string phone, Guid Id)[]
            {
                ("indrekkann@email.com", "Aspnet123!", "Indrek", "Kann", "5555555",
                    new Guid("00000000-0000-0000-0000-000000000001")),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        Phone = userInfo.phone,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }

        }
    }
}