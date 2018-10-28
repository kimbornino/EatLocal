using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal.Models
{
    public class LocalMarkets
    {
            [Key]
            public int ID { get; set; }

            public string Name { get; set; }

            [Display(Name="Season Start Date")]
            public DateTime SeasonOpen { get; set; }

            [Display(Name="Season Close Date")]
            public DateTime SeasonClose { get; set; }

            public string Link { get; set; }

            [Display(Name="Market Information")]
            public string Bio { get; set; }

            [Display(Name="Street Address")]
            public string StreetAddress { get; set; }

             [Display(Name = "City, State, Zip")]
             public string CityStateZip { get; set; }

            public int? MondayStart { get; set; }

            public int? MondayEnd { get; set; }

            public int? TuesdayStart { get; set; }

            public int? TuesdayEnd { get; set; }

            public int? WednesayStart { get; set; }

            public int? WednesdayEnd { get; set; }

            public int? ThursdayStart { get; set; }

            public int? ThursdayEnd { get; set; }

            public int? FridayStart { get; set; }

            public int? FridayEnd { get; set; }

            public int? SaturdayStart { get; set; }

            public int? SaturdayEnd { get; set; }

            public int? SundayStart { get; set; }

            public int? SundayEnd { get; set; }

        }
    }
