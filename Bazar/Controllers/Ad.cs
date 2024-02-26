using AutoMapper;
using Bazar.Controllers;
using Bazar.Models;
using Bazar.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bazar.Controllers;

public class Ad : BaseController
{
    private readonly IAdService _adService;
    private readonly ICategoryService categoryService;
    private readonly IAdSearchService _adSearchService;
    private readonly IMapper _mapper;


    public Ad(IAdService adService, ICategoryService categoryService, IAdSearchService adSearchService, IMapper mapper)
    {
        _adService = adService;
        this.categoryService = categoryService;
        _adSearchService = adSearchService;
        _mapper = mapper;
    }

    public IActionResult My_Ads(int page = 1)
    {
        var userId = GetUserId();
        int pageSize = 6; // Number of items per page
        var ads = _adService.My_Ads(userId, page, pageSize);

        ViewBag.TotalItems = _adService.GetTotalAdsCount();
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        return View(ads);
    }

    [AllowAnonymous]
    public IActionResult All_Unregistered(int page = 1)
    {
        int pageSize = 18; // Number of items per page
        var ads = _adService.GetAllAds(page, pageSize);

        ViewBag.TotalItems = _adService.GetTotalAdsCount();
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        return View(ads);
    }

    [HttpGet]
    public IActionResult Search(AdSearchViewModel searchModel, int page = 1)
    {
        int pageSize = 18; // Number of items per page
        List<AdViewModel> adViewModels = _adSearchService.SearchAds(searchModel, page, pageSize);

        // Retrieve the categories and populate the Categories property
        var categories = categoryService.GetAllCategories().ToList();

        var model = new AdSearchViewModel
        {
            Ads = adViewModels,
            Categories = categories
        };

        ViewBag.TotalItems = _adSearchService.GetTotalAdsCount(searchModel);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var categories = this.categoryService.GetAllCategories();

        var viewModel = new AdAddEditViewModel
        {
            Categories = categories
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Add(AdAddEditViewModel model)
    {
        var userId = GetUserId();
        model.Categories = _adService.GetAllCategories();

        _adService.AddAd(model, userId);
        ViewBag.Title = "Selected Ads for You";
        return RedirectToAction("My_Ads");
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var userId = GetUserId();
        _adService.AddToCart(id, userId);
        return RedirectToAction("Cart");
    }

    public IActionResult Cart()
    {
        var userId = GetUserId();

        var cartItems = _adService.GetCartItems(userId);

        return View(cartItems);
    }


    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _adService.RemoveFromCart(id, userId);

        return RedirectToAction("Cart");
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var model = _adService.GetAdForEdit(id);
        if (model == null)
        {
            return NotFound();
        }
        model.Categories = _adService.GetAllCategories();

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(AdAddEditViewModel model)
    {
        model.Categories = _adService.GetAllCategories();

        _adService.EditAd(model);

        return RedirectToAction("My_Ads");
    }

    public IActionResult Delete(int id)
    {
        _adService.DeleteAd(id);

        return RedirectToAction("My_Ads");
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = categoryService.GetAllCategories().ToList();
        return Json(categories);
    }

}