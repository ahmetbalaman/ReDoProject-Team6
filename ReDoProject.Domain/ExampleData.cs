using System;
using ReDoProject.Domain.Entities;
using ReDoProject.Domain.Enums;

namespace ReDoProject.Domain
{
    public static class ExampleData
    {
        public static List<Instrument> GetInstruments()
        {
            var instruments = new List<Instrument>();

            instruments.Add(new Instrument
            {
                Name = "Elektro Gitar",
                Description = "6 telli elektro gitar",
                Brand = new Brand
                {
                    Name = "Fender",
                    DisplayingText = "Fender Corporation",
                    Address = "1234 Guitar Street, Los Angeles, CA",
                    SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454"
                },
                Price = 800.00m,
                Color = { Color.Red },
                Barcode = "GTRE123456",
                PictureUrl = "guitar.jpg",
                Type = InstrumentType.ElectricGuitar
            });

            instruments.Add(new Instrument
            {
                Name = "Dijital Piyano",
                Description = "88 tuşlu dijital piyano",
                Brand = new Brand
                {
                    Name = "Yamaha",
                    DisplayingText = "Yamaha Music Corporation",
                    Address = "5678 Piano Avenue, Tokyo, Japan",
                       SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454"
                },
                Price = 1200.00m,
                Color = { Color.Black },
                Barcode = "PIYN789012",
                PictureUrl = "piano.jpg",
                Type = InstrumentType.AcousticPiano
            });

            instruments.Add(new Instrument
            {
                Name = "Akustik Keman",
                Description = "4 telli akustik keman",
                Brand = new Brand
                {
                    Name = "Stradivarius",
                    DisplayingText = "Stradivarius Violins",
                    Address = "4321 Violin Lane, Cremona, Italy",
                       SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454"
                },
                Price = 500.00m,
                Color = { Color.Brown },
                Barcode = "KEMN345678",
                PictureUrl = "violin.jpg",
                Type = InstrumentType.Violin
            });

            instruments.Add(new Instrument
            {
                Name = "Akustik Davul Seti",
                Description = "5 parça akustik davul seti",
                Brand = new Brand
                {
                    Name = "Pearl",
                    DisplayingText = "Pearl Drums",
                    Address = "789 Drum Street, Tokyo, Japan",
                       SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454"
                },
                Price = 900.00m,
                Color = { Color.Gray },
                Barcode = "DVLS234567",
                PictureUrl = "drum.jpg",
                Type = InstrumentType.DrumSet
            });

            instruments.Add(new Instrument
            {
                Name = "Trompet",
                Description = "Bb anahtarlı trompet",
                Brand = new Brand
                {
                    Name = "Bach",
                    DisplayingText = "Bach Brass Instruments",
                    Address = "456 Brass Avenue, New York, USA",
                    SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454",
                },
                Price = 300.00m,
                Color = { Color.Gold },
                Barcode = "TRMP456789",
                PictureUrl = "trumpet.jpg",
                Type = InstrumentType.Trumpet
            });

            return instruments;
        }
    }

}