using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assign1_Salesboard_Zephyr.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Zephyr_ApplicationUser> _userManager;
        private readonly SignInManager<Zephyr_ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<Zephyr_ApplicationUser> userManager,
            SignInManager<Zephyr_ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [StringLength(25)]
            [Display(Name = "First Name")]
            public string Fname { get; set; }

            [StringLength(50)]
            [Display(Name = "Last Name")]
            public string Lname { get; set; }

            [Range(16, 120)]
            [Display(Name = "Age")]
            public int Age { get; set; }

            public string Image { get; set; }

            public string State { get; set; }

            public string City { get; set; }

            public string Postcode { get; set; }

            public string Street { get; set; }
        }

        private async Task LoadAsync(Zephyr_ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Fname = user.Fname,
                Lname = user.Lname,
                Age = user.Age,
                Image = user.Image,
                State = user.State,
                City = user.City,
                Postcode = user.Postcode,
                Street = user.Street
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Fname != user.Fname)
            {
                user.Fname = Input.Fname;
            }

            if (Input.Lname != user.Lname)
            {
                user.Lname = Input.Lname;
            }

            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
            }

            if (Input.Image != user.Image)
            {
                user.Image = Input.Image;
            }

            if (Input.State != user.State)
            {
                user.State = Input.State;
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
            }

            if (Input.Postcode != user.Postcode)
            {
                user.Postcode = Input.Postcode;
            }

            if (Input.Street != user.Street)
            {
                user.Street = Input.Street;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
