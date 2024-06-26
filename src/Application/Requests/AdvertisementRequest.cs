﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Requests;
public class AdvertisementRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageBanner { get; set; }
    public string? ImageCard { get; set; }
    public bool? Enabled { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? UrlGoTo { get; set; }
}
