﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.ViewModels
{
    public class ReportVM
    {
        [Display(Name = "Год")] public int year { get; set; }
        [Display(Name = "Месяц")] public string? month { get; set; }
        [Display(Name = "Полное имя"), Required(ErrorMessage = "Выберите пользователя")] public string? fullName { get; set; }
        [Display(Name = "Категория")] public string? category { get; set; }
        public IEnumerable<SelectListItem>? FullNameList { get; set; }
        public IEnumerable<SelectListItem>? YearList { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        public Months Months { get; set; }   
    }
    public enum Months
    {
        Январь,
        Февраль,
        Март,
        Апрель,
        Май,
        Июнь,
        Июль,
        Август,
        Сентябрь,
        Октябрь,
        Ноябрь,
        Декабрь
    }
}