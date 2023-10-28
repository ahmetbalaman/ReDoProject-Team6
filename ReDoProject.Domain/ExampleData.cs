using System;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Entities;
using ReDoProject.Domain.Enums;

namespace ReDoProject.Domain
{
    public static class ExampleData
    {
        public static List<Brand> GetBrands()
        {
            var brand = new List<Brand>();

            brand.Add(
                new Brand
                {

                    Name = "Fender",
                    DisplayingText = "Fender Corporation",
                    Address = "1234 Guitar Street, Los Angeles, CA",
                    SupportMail = "blabla@gmail.com",
                    SupportPhone = "05454545454",
                    CreatedOn = DateTime.UtcNow
                });
            brand.Add(

             new Brand
             {

                 Name = "Yamaha",
                 DisplayingText = "Yamaha Music Corporation",
                 Address = "5678 Piano Avenue, Tokyo, Japan",
                 SupportMail = "blabla@gmail.com",
                 SupportPhone = "05454545454",
                 CreatedOn = DateTime.UtcNow
             });

            return brand;
        }

        public static List<Instrument> GetInstruments()
        {
            var instruments = new List<Instrument>();


            instruments.Add(new Instrument
            {
                Id = Guid.NewGuid(),
                Name = "Elektro Gitar",
                Description = "6 telli elektro gitar",
                CreatedOn = DateTime.UtcNow,
                Brand = GetBrands()[0],
                Price = 800.00m,
                Color =new List<Color>() { Color.Red },
                Barcode = "GTRE123456",
                PictureUrl = "https://media.istockphoto.com/id/156547833/tr/foto%C4%9Fraf/acoustic-guitar.jpg?s=612x612&w=0&k=20&c=VmDmUJNdn4OJitnDyyQxIpfL7AVzA-tVcvCF_Zz1akw=",
                Type = InstrumentType.ElectricGuitar
            }); ;

            instruments.Add(new Instrument
            {
                Id = Guid.NewGuid(),
                Name = "Dijital Piyano",
                Description = "88 tuşlu dijital piyano",
                Brand = GetBrands()[1],
                Price = 1200.00m,
                CreatedOn = DateTime.UtcNow,
                Color = new List<Color>() { Color.Black },
                Barcode = "PIYN789012",
                PictureUrl = "https://cdn.pixabay.com/photo/2018/06/29/01/47/piano-3505109_640.jpg",
                Type = InstrumentType.AcousticPiano
            });

            instruments.Add(new Instrument
            {
                Id = Guid.NewGuid(),
                Name = "Akustik Keman",
                Description = "4 telli akustik keman",
                CreatedOn = DateTime.UtcNow,
                Brand = GetBrands()[1],
                Price = 500.00m,
                Color = new List<Color>() { Color.Brown },
                Barcode = "KEMN345678",
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Old_violin.jpg/250px-Old_violin.jpg",
                Type = InstrumentType.Violin
            });

            instruments.Add(new Instrument
            {
                Id = Guid.NewGuid(),
                Name = "Akustik Davul Seti",
                Description = "5 parça akustik davul seti",
                CreatedOn = DateTime.UtcNow,
                Brand = GetBrands()[0],
                Price = 900.00m,
                Color = new List<Color>() { Color.Gray },
                Barcode = "DVLS234567",
                PictureUrl = "https://onlinemuzikkursu.com/tema/genel/uploads/urunler/2_Ludwig-Accent-Fuse-Akustik-Davul-Seti-Kirmizi-resim2-5850-2.jpg",
                Type = InstrumentType.DrumSet
            });

            instruments.Add(new Instrument
            {
                Id = Guid.NewGuid(),
                Name = "Trompet",
                Description = "Bb anahtarlı trompet",
                CreatedOn = DateTime.UtcNow,
                Brand = GetBrands()[0],
                Price = 300.00m,
                Color = new List<Color>() { Color.Gold },
                Barcode = "TRMP456789",
                PictureUrl = "https://m.media-amazon.com/images/I/71XiJcmLuGL._AC_UF1000,1000_QL80_.jpg",
                Type = InstrumentType.Trumpet
            });

            return instruments;
        }
    }

}