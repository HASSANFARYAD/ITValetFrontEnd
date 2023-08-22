﻿using ITValetFrontEnd.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITValetFrontEnd.Models
{
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class PostAddUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Timezone { get; set; }
        public string? Availability { get; set; }
        public string? Status { get; set; }
        public string? Language { get; set; }
        public string? Gender { get; set; }
        public string? StripeId { get; set; }
        public string? PricePerHour { get; set; }
        public string? SignUpOption { get; set; }
    }

    public class PostUpdateUserDto
    {
        public int? Id { get; set; }
        public string? UserEncId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Timezone { get; set; }
        public string? Availability { get; set; }
        public string? Status { get; set; }
        public string? Language { get; set; }
        public string? Gender { get; set; }
        public string? PricePerHour { get; set; }
        public string? Role { get; set; }
        public string? AvailabilityType { get; set; }
        public string? SelectedDays { get; set; }
        public string? AvailableTimeSlots { get; set; }
        public string? Description { get; set; }
    }

    public class UserListDto
    {
        public int? Id { get; set; }
        public string? UserEncId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Timezone { get; set; }
        public string? Availability { get; set; }
        public string? Status { get; set; }
        public string? Language { get; set; }
        public string? Gender { get; set; }
        public string? StripeId { get; set; }
        public string? PricePerHour { get; set; }
        public string? IsActive { get; set; }
        public string? AvailabilitySlots { get; set; }
        public string? Description { get; set; }
        public string? CurrentTime { get; set; }
    }

    public class UserClaims
    {
        public int? Id { get; set; }
        public string? UserEncId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? Role { get; set; }
        public string? BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Timezone { get; set; }
        public int? Availability { get; set; }
        public string? Status { get; set; }
        public string? Language { get; set; }
        public string? Gender { get; set; }
        public string? StripeId { get; set; }
        public string? PricePerHour { get; set; }
        public string? Token { get; set; }
        public string? IsActive { get; set; }
    }

    public class ResponseDto
    {
        public dynamic? Data { get; set; }
        public bool? Status { get; set; }
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class UpdatePasswordDto
    {
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
    }

    public class ResetPasswordDto
    {
        public string? UserId { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class UserValidation
    {
        public int? Id { get; set; }
        public string? UserEncId { get; set; }
        public string? UserName { get; set; }
        public string? UserProfile { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string? Email { get; set; }
    }

    #region Education
    public class PostAddUserEducation
    {
        public string? DegreeName { get; set; }
        public string? InstituteName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? UserId { get; set; }
    }

    public class PostUpdateUserEducation
    {
        public int? Id { get; set; }
        public string? UserEducationEncId { get; set; }
        public string? DegreeName { get; set; }
        public string? InstituteName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? UserId { get; set; }
    }

    public class UserEducationDto
    {
        public int? Id { get; set; }
        public string? UserEducationEncId { get; set; }
        public string? DegreeName { get; set; }
        public string? InstituteName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region Experience
    public class PostAddUserExperience
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ExperienceFrom { get; set; }
        public string? ExperienceTo { get; set; }
        public string? Organization { get; set; }
        public string? Website { get; set; }
        public int? UserId { get; set; }
    }

    public class PostUpdateUserExperience
    {
        public int? Id { get; set; }
        public string? UserExperienceEncId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ExperienceFrom { get; set; }
        public string? ExperienceTo { get; set; }
        public string? Organization { get; set; }
        public string? Website { get; set; }
        public int? UserId { get; set; }
    }

    public class UserExperienceDto
    {
        public int? Id { get; set; }
        public string? UserExperienceEncId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ExperienceFrom { get; set; }
        public string? ExperienceTo { get; set; }
        public string? Organization { get; set; }
        public string? Website { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region SocialProfile
    public class PostAddUserSocialProfile
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public int? UserId { get; set; }
    }

    public class PostUpdateUserSocialProfile
    {
        public int? Id { get; set; }
        public string? UserSocialProfileEncId { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public int? UserId { get; set; }
    }

    public class UserSocialProfileDto
    {
        public int? Id { get; set; }
        public string? UserSocialProfileEncId { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region Skill
    public class PostAddUserSkill
    {
        public string? SkillName { get; set; }
        public int? UserId { get; set; }
    }


    public class PostUpdateUserSkill
    {
        public int? Id { get; set; }
        public string? UserSkillEncId { get; set; }
        public string? SkillName { get; set; }
        public int? UserId { get; set; }
    }

    public class UserSkillDto
    {
        public int? Id { get; set; }
        public string? UserSkillEncId { get; set; }
        public string? SkillName { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region Tag
    public class PostAddUserTag
    {
        public string? TagName { get; set; }
        public int? UserId { get; set; }
    }

    public class PostUpdateUserTag
    {
        public int? Id { get; set; }
        public string? UserTagEncId { get; set; }
        public string? TagName { get; set; }
        public int? UserId { get; set; }
    }

    public class UserTagDto
    {
        public int? Id { get; set; }
        public string? UserTagEncId { get; set; }
        public string? TagName { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region AvailableSlot
    public class PostAddUserAvailableSlot
    {
        public string? DateTimeOfDay { get; set; }
        public string? Slot1 { get; set; }
        public string? Slot2 { get; set; }
        public string? Slot3 { get; set; }
        public string? Slot4 { get; set; }
        public int? UserId { get; set; }
    }

    public class PostUpdateUserAvailableSlot
    {
        public int? Id { get; set; }
        public string? UserAvailableSlotEncId { get; set; }
        public string? DateTimeOfDay { get; set; }
        public string? Slot1 { get; set; }
        public string? Slot2 { get; set; }
        public string? Slot3 { get; set; }
        public string? Slot4 { get; set; }
        public int? UserId { get; set; }
    }

    public class UserAvailableSlotDto
    {
        public int? Id { get; set; }
        public string? UserAvailableSlotEncId { get; set; }
        public string? DateTimeOfDay { get; set; }
        public string? Slot1 { get; set; }
        public string? Slot2 { get; set; }
        public string? Slot3 { get; set; }
        public string? Slot4 { get; set; }
        public int? UserId { get; set; }
    }
    #endregion

    #region RequestServices
    public class PostAddRequestServices
    {
        public string? ServiceTitle { get; set; }
        public string? PrefferedServiceTime { get; set; }
        public string? CategoriesOfProblems { get; set; }
        public string? ServiceDescription { get; set; }
        public string? FromDateTime { get; set; }
        public string? ToDateTime { get; set; }
        public string? RequestServiceSkills { get; set; }
        public string? ServiceLanguage { get; set; } // It can be multiple by , seperated
        public string? RequestedServiceUserId { get; set; }
        public string? RequestServiceType { get; set; } // 1 Live Now 2 Schedule Later
    }

    public class PostUpdateRequestServices
    {
        public string? Id { get; set; }
        public string? EncId { get; set; }
        public string? ServiceTitle { get; set; }
        public string? PrefferedServiceTime { get; set; }
        public string? CategoriesOfProblems { get; set; }
        public string? ServiceDescription { get; set; }
        public string? FromDateTime { get; set; }
        public string? ToDateTime { get; set; }
        public string? RequestServiceSkills { get; set; }
        public string? ServiceLanguage { get; set; } // It can be multiple by , seperated
        public string? RequestedServiceUserId { get; set; }
        public string? RequestServiceType { get; set; } // 1 Live Now 2 Schedule Later
        public string? RequestServiceSkill { get; set; }
    }

    public class RequestServicesDto
    {
        public int? Id { get; set; }
        public string? EncId { get; set; }
        public string? ServiceTitle { get; set; }
        public string? PrefferedServiceTime { get; set; }
        public string? CategoriesOfProblems { get; set; }
        public string? ServiceDescription { get; set; }
        public string? FromDateTime { get; set; }
        public string? ToDateTime { get; set; }
        public string? RequestServiceSkills { get; set; }
        public string? ServiceLanguage { get; set; } // It can be multiple by , seperated
        public string? RequestedServiceUserId { get; set; }
        public string? RequestedServiceUserName { get; set; }
        public string? RequestServiceType { get; set; } // 1 Live Now 2 Schedule Later
    }

    #endregion

    #region OrderViewModels
    public class OrderDtoList
    {
        public string? Id { get; set; }
        public string? EncId { get; set; }
        public string? OrderTitle { get; set; }
        public string? OrderDescription { get; set; }
        public string? OrderPrice { get; set; }
        public string? OrderStatus { get; set; } // 0 Pending, 1 Accept, 2 Cancelled, 3 Delivered, 4 Approved
        public string? StripeChargeId { get; set; }
        public string? StartDateTime { get; set; }
        public string? EndDateTime { get; set; }
        public string? IsDelivered { get; set; }
        public string? RequestId { get; set; }
        public string? CustomerId { get; set; }
        public string? ValetId { get; set; }
        public string? PackageId { get; set; }
        public string? OrderReasonId { get; set; }
        public string? OrderReasonType { get; set; }
        public string? OrderReasonExplanation { get; set; }
        public string? OrderReasonIsActive { get; set; }
        public string? UserName { get; set; }
        public string? CreatedAt { get; set; }
    }
    #endregion

    #region CheckouPayment
    public class CheckOutDTO
    {
        public string? Id { set; get; }
        public string? FromDateTime { set; get; }
        public string? ToDateTime { set; get; }
        public string? title { set; get; }
        public string? desc { set; get; }
        public string? duration { set; get; }
        public string? orderId { set; get; }
        public string? customerId { set; get; }
        public string? ValetId { set; get; }
        public string? hourlyRate { set; get; }
        public string? stripeFee { set; get; }
        public string? platformFee { set; get; }
        public string? wHours { set; get; }
        public string? workCharges { set; get; }
        public string? stripeEmail { set; get; }
        public string? stripeToken { set; get; }
    }

    #endregion

    #region PayPalViewModel
    public class PackageCheckOutViewModel
    {
        public int ClientId { get; set; }
        public string? PackageType { get; set; }
        public decimal PackagePrice { get; set; }
        public string? PayableAmount { get; set; }
        public string? Currency { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentId { get; set; }
        public int IsActive { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
    public class OrderCheckOutViewModel
    {
        public int ClientId { get; set; }
        public int ValetId { get; set; }
        public string? CaptureId { get; set; }
        public string? AuthorizationId { get; set; }
        public string? Currency { get; set; }
        public int OrderId { get; set; }
        public decimal? OrderPrice { get; set; }
        public string? PayableAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentId { get; set; }
        public bool IsRefund { get; set; }
    }
    public class PayPalFundToValetViewModel
    {
        public int OrderId { get; set; }
        public string? PaymentId { get; set; }
        public int ClientId { get; set; }
        public string? PayPalAccEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public double PlatformFee { get; set; }
        public decimal SentPayment { get; set; }
        public int ValetId { get; set; }
        public string? BatchId { get; set; }
        public string? PayOutItemId { get; set; }
        public string? TransactionStatus { get; set; }
        public int OrderCheckOutId { get; set; }
        public bool CancelByAdmin { get; set; }
        public string? CancelationReason { get; set; }
        public string? CancelationStatus { get; set; }
        public string? ReturnedAmount { get; set; }
    }
    public class PaymentCancelViewModel
    {
        public bool CancelByAdmin { get; set; }
        public string? CancelationReason { get; set; }
        public string? CancelationStatus { get; set; }
        public string? ReturnedAmount { get; set; }
    }
    public class PayPalOrderCheckOutViewModel
    {
        public int ClientId { get; set; }
        public int ValetId { get; set; }
        public int OfferId { get; set; }
        public int OrderId { get; set; }
        public string? OrderTitle { get; set; }
        public string? OrderDescription { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class CaptureAmountViewModel
    {
        public string? PayableAmount { get; set; }
        public string? Currency { get; set; }
    }
    public class PayPalAccountViewModel
    {
        public int ValetId { get; set; }
        public string? PayPalEmail { get; set; }
        public bool IsPayPalAuthorized { get; set; }
        public int IsActive { get; set; }
    }
    public class AddPayPalResult
    {
        public bool Success { get; }
        public string Message { get; }
        public AddPayPalResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
    public class TransactionDetailViewModel
    {
        public string TransactionId { get; set; }
        public string State { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        // Add other relevant properties you want to display
    }
    public class PayPalCheckOutURL
    {
        public string Url { get; set; }
    }
    public class PackageCOutRequest
    {
        public int ClientId { get; set; }
        public string? SelectedPackage { get; set; }
    }
    public class CheckoutPaymentStatusPackage
    {
        public string? PaymentStatus { get; set; }
        public string? PaymentId { get; set; }
    }
    public class CheckoutPaymentStatusOrder
    {
        public string? PaymentStatus { get; set; }
        public string? PaymentId { get; set; }
        public string? AuthorizationId { get; set; }
        public string? CaptureId { get; set; }
    }
    public class CancelOrder
    {
        public int OrderId { get; set; }
        public string? CapturedId { get; set; }
    }
    #endregion
}
