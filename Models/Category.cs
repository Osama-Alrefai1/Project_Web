﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string? Name { get; set; }

        public ICollection<Item>? Items { get; set; }

        [NotMapped]
        public IFormFile clientFile { get; set; }


        public byte[]? dbImage { get; set; }


        public string? ImageSrc
        {
            get
            {
                if (dbImage != null)
                {
                    string base64String = Convert.ToBase64String(dbImage , 0 , dbImage.Length);
                    return "data:image/jpg;base64," + base64String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
