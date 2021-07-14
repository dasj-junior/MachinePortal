using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MachinePortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MachinePortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MachinePortalUser> _signInManager;
        private readonly UserManager<MachinePortalUser> _userManager;
        private readonly IdentityContext _context;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _appEnvironment;

                public RegisterModel(
            IHostingEnvironment enviroment,
            UserManager<MachinePortalUser> userManager,
            SignInManager<MachinePortalUser> signInManager,
            ILogger<RegisterModel> logger,
            IdentityContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _appEnvironment = enviroment;
            _context = context;
        }

        public string confirmPageP1 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional //EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">

<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml"">
<head>
<!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG/><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]-->
<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""/>
<meta content=""width=device-width"" name=""viewport""/>
<!--[if !mso]><!-->
<meta content=""IE=edge"" http-equiv=""X-UA-Compatible""/>
<!--<![endif]-->
<title></title>
<!--[if !mso]><!-->
<link href=""https://fonts.googleapis.com/css?family=Cabin"" rel=""stylesheet"" type=""text/css""/>
<!--<![endif]-->
<style type=""text/css"">
		body {
			margin: 0;
			padding: 0;
		}

		table,
		td,
		tr {
			vertical-align: top;
			border-collapse: collapse;
		}

		* {
			line-height: inherit;
		}

		a[x-apple-data-detectors=true] {
			color: inherit !important;
			text-decoration: none !important;
		}
	</style>
<style id=""media-query"" type=""text/css"">
		@media (max-width: 620px) {

			.block-grid,
			.col {
				min-width: 320px !important;
				max-width: 100% !important;
				display: block !important;
			}

			.block-grid {
				width: 100% !important;
			}

			.col {
				width: 100% !important;
			}

			.col_cont {
				margin: 0 auto;
			}

			img.fullwidth,
			img.fullwidthOnMobile {
				max-width: 100% !important;
			}

			.no-stack .col {
				min-width: 0 !important;
				display: table-cell !important;
			}

			.no-stack.two-up .col {
				width: 50% !important;
			}

			.no-stack .col.num2 {
				width: 16.6% !important;
			}

			.no-stack .col.num3 {
				width: 25% !important;
			}

			.no-stack .col.num4 {
				width: 33% !important;
			}

			.no-stack .col.num5 {
				width: 41.6% !important;
			}

			.no-stack .col.num6 {
				width: 50% !important;
			}

			.no-stack .col.num7 {
				width: 58.3% !important;
			}

			.no-stack .col.num8 {
				width: 66.6% !important;
			}

			.no-stack .col.num9 {
				width: 75% !important;
			}

			.no-stack .col.num10 {
				width: 83.3% !important;
			}

			.video-block {
				max-width: none !important;
			}

			.mobile_hide {
				min-height: 0px;
				max-height: 0px;
				max-width: 0px;
				display: none;
				overflow: hidden;
				font-size: 0px;
			}

			.desktop_hide {
				display: block !important;
				max-height: none !important;
			}
		}
	</style>
<style id=""icon-media-query"" type=""text/css"">
		@media (max-width: 620px) {
			.icons-inner {
				text-align: center;
			}

			.icons-inner td {
				margin: 0 auto;
			}
		}
	</style>
</head>
<body class=""clean-body"" style=""margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #ffffff;"">
<!--[if IE]><div class=""ie-browser""><![endif]-->
<table bgcolor=""#ffffff"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation"" style=""table-layout: fixed; vertical-align: top; min-width: 320px; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #ffffff; width: 100%;"" valign=""top"" width=""100%"">
<tbody>
<tr style=""vertical-align: top;"" valign=""top"">
<td style=""word-break: break-word; vertical-align: top;"" valign=""top"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td align=""center"" style=""background-color:#fbfaf1""><![endif]-->
<div style=""background-color:transparent;"">
<div class=""block-grid"" style=""min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; Margin: 0 auto; background-color: #F0E614;"">
<div style=""border-collapse: collapse;display: table;width: 100%;background-color:#F0E614;"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:transparent;""><tr><td align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:600px""><tr class=""layout-full-width"" style=""background-color:#F0E614""><![endif]-->
<!--[if (mso)|(IE)]><td align=""center"" width=""600"" style=""background-color:#F0E614;width:600px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;"" valign=""top""><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 0px; padding-left: 20px; padding-top:5px; padding-bottom:5px;""><![endif]-->
<div class=""col num12"" style=""min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top; width: 600px;"">
<div class=""col_cont"" style=""width:100% !important;"">
<!--[if (!mso)&(!IE)]><!-->
<div style=""border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 20px;"">
<!--<![endif]-->
<div align=""center"" class=""img-container center fixedwidth"" style=""padding-right: 25px;padding-left: 25px;"">
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr style=""line-height:0px""><td style=""padding-right: 25px;padding-left: 25px;"" align=""center""><![endif]-->
<div style=""font-size:1px;line-height:25px""> </div><img align=""center"" alt=""Image"" border=""0"" class=""center fixedwidth"" src=""https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&amp;inputFormat=png&amp;cs=fFNQTw&amp;docid=https%3A%2F%2Fvitesco-my.sharepoint.com%3A443%2F_api%2Fv2.0%2Fdrives%2Fb!mMdYw1SQ-0erpy8hbET0tIalLhKYWZhAp8hT60i5bsiRLizQxH2MSLkH0GVZ0gJd%2Fitems%2F01K7VQC6L7V4DI34OFVBHKOYQ5K5XBWLKD%3Fversion%3DPublished&amp;access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvdml0ZXNjby1teS5zaGFyZXBvaW50LmNvbUAzOWI3NzEwMS05OWI3LTQxYzktOGQ2YS03Nzk0YjlkNDg0NzYiLCJpc3MiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAiLCJuYmYiOiIxNjIzMzE1NjAwIiwiZXhwIjoiMTYyMzMzNzIwMCIsImVuZHBvaW50dXJsIjoiZVU3eVhqdTAwUjJvcnoyOGNxRlN1UXdWQm9KMWRmdWh1ejJVU2lZclhoYz0iLCJlbmRwb2ludHVybExlbmd0aCI6IjExNyIsImlzbG9vcGJhY2siOiJUcnVlIiwidmVyIjoiaGFzaGVkcHJvb2Z0b2tlbiIsInNpdGVpZCI6Ill6TTFPR00zT1RndE9UQTFOQzAwTjJaaUxXRmlZVGN0TW1ZeU1UWmpORFJtTkdJMCIsInNpZ25pbl9zdGF0ZSI6IltcImttc2lcIixcImR2Y19kbWpkXCJdIiwibmFtZWlkIjoiMCMuZnxtZW1iZXJzaGlwfHVpZGo3ODgyQHZpdGVzY28uY29tIiwibmlpIjoibWljcm9zb2Z0LnNoYXJlcG9pbnQiLCJpc3VzZXIiOiJ0cnVlIiwiY2FjaGVrZXkiOiIwaC5mfG1lbWJlcnNoaXB8MTAwMzIwMDBkNTZmNjhjZkBsaXZlLmNvbSIsInR0IjoiMCIsInVzZVBlcnNpc3RlbnRDb29raWUiOiIzIn0.WXVLWEExWG83cklxMG5kUWdaN2JCNnRtY2ZDQi9PSWZIMm5rS1dlZTE1RT0&amp;encodeFailures=1&amp;width=1920&amp;height=1013&amp;srcWidth=&amp;srcHeight="" style=""text-decoration: none; -ms-interpolation-mode: bicubic; height: auto; border: 0; width: 100%; max-width: 530px; display: block;"" title=""Image"" width=""530""/>
<div style=""font-size:1px;line-height:25px""> </div>
<!--[if mso]></td></tr></table><![endif]-->
</div>
<!--[if (!mso)&(!IE)]><!-->
</div>
<!--<![endif]-->
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
</div>
</div>
</div>
<div style=""background-color:transparent;"">
<div class=""block-grid"" style=""min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; Margin: 0 auto; background-color: #FFFFFF;"">
<div style=""border-collapse: collapse;display: table;width: 100%;background-color:#FFFFFF;"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:transparent;""><tr><td align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:600px""><tr class=""layout-full-width"" style=""background-color:#FFFFFF""><![endif]-->
<!--[if (mso)|(IE)]><td align=""center"" width=""600"" style=""background-color:#FFFFFF;width:600px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;"" valign=""top""><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 35px; padding-left: 35px; padding-top:35px; padding-bottom:40px;""><![endif]-->
<div class=""col num12"" style=""min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top; width: 600px;"">
<div class=""col_cont"" style=""width:100% !important;"">
<!--[if (!mso)&(!IE)]><!-->
<div style=""border-top:0px solid transparent; border-left:1px solid #F0E614; border-bottom:0px solid transparent; border-right:1px solid #F0E614; padding-top:35px; padding-bottom:40px; padding-right: 35px; padding-left: 35px;"">
<!--<![endif]-->
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 10px; padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-family: Arial, sans-serif""><![endif]-->
<div style=""color:#132F40;font-family:Cabin, Arial, Helvetica Neue, Helvetica, sans-serif;line-height:1.2;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; line-height: 1.2; color: #132F40; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; mso-line-height-alt: 14px;"">
<p style=""margin: 0; font-size: 22px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 26px; margin-top: 0; margin-bottom: 0;""><span style=""font-size: 22px;"">Click bellow to complete your registration on Machine Portal</span></p>
</div>
</div>
<!--[if mso]></td></tr></table><![endif]-->
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 10px; padding-left: 10px; padding-top: 5px; padding-bottom: 30px; font-family: Arial, sans-serif""><![endif]-->
<div style=""color:#555555;font-family:Cabin, Arial, Helvetica Neue, Helvetica, sans-serif;line-height:1.5;padding-top:5px;padding-right:10px;padding-bottom:30px;padding-left:10px;"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; line-height: 1.5; color: #555555; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; mso-line-height-alt: 18px;"">
<p style=""margin: 0; font-size: 14px; line-height: 1.5; word-break: break-word; mso-line-height-alt: 21px; margin-top: 0; margin-bottom: 0;"">Thanks for registering your account with us, to complete the process and be able to access all features of Machine Portal, please click on button bellow to confirm your account creation.</p>
</div>
</div>
<!--[if mso]></td></tr></table><![endif]-->
<div align=""center"" class=""img-container center fixedwidth"" style=""padding-right: 0px;padding-left: 0px;"">
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr style=""line-height:0px""><td style=""padding-right: 0px;padding-left: 0px;"" align=""center""><![endif]--><img align=""center"" alt=""Image"" border=""0"" class=""center fixedwidth"" src=""https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&amp;inputFormat=png&amp;cs=fFNQTw&amp;docid=https%3A%2F%2Fvitesco-my.sharepoint.com%3A443%2F_api%2Fv2.0%2Fdrives%2Fb!mMdYw1SQ-0erpy8hbET0tIalLhKYWZhAp8hT60i5bsiRLizQxH2MSLkH0GVZ0gJd%2Fitems%2F01K7VQC6LMLBV3CQOVHREZ6ONEJTGB6A7S%3Fversion%3DPublished&amp;access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvdml0ZXNjby1teS5zaGFyZXBvaW50LmNvbUAzOWI3NzEwMS05OWI3LTQxYzktOGQ2YS03Nzk0YjlkNDg0NzYiLCJpc3MiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAiLCJuYmYiOiIxNjIzMzE1NjAwIiwiZXhwIjoiMTYyMzMzNzIwMCIsImVuZHBvaW50dXJsIjoiZVU3eVhqdTAwUjJvcnoyOGNxRlN1UXdWQm9KMWRmdWh1ejJVU2lZclhoYz0iLCJlbmRwb2ludHVybExlbmd0aCI6IjExNyIsImlzbG9vcGJhY2siOiJUcnVlIiwidmVyIjoiaGFzaGVkcHJvb2Z0b2tlbiIsInNpdGVpZCI6Ill6TTFPR00zT1RndE9UQTFOQzAwTjJaaUxXRmlZVGN0TW1ZeU1UWmpORFJtTkdJMCIsInNpZ25pbl9zdGF0ZSI6IltcImttc2lcIixcImR2Y19kbWpkXCJdIiwibmFtZWlkIjoiMCMuZnxtZW1iZXJzaGlwfHVpZGo3ODgyQHZpdGVzY28uY29tIiwibmlpIjoibWljcm9zb2Z0LnNoYXJlcG9pbnQiLCJpc3VzZXIiOiJ0cnVlIiwiY2FjaGVrZXkiOiIwaC5mfG1lbWJlcnNoaXB8MTAwMzIwMDBkNTZmNjhjZkBsaXZlLmNvbSIsInR0IjoiMCIsInVzZVBlcnNpc3RlbnRDb29raWUiOiIzIn0.WXVLWEExWG83cklxMG5kUWdaN2JCNnRtY2ZDQi9PSWZIMm5rS1dlZTE1RT0&amp;encodeFailures=1&amp;width=1920&amp;height=956&amp;srcWidth=&amp;srcHeight="" style=""text-decoration: none; -ms-interpolation-mode: bicubic; height: auto; border: 0; width: 100%; max-width: 530px; display: block;"" title=""Image"" width=""530""/>
<!--[if mso]></td></tr></table><![endif]-->
</div>
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 10px; padding-left: 10px; padding-top: 20px; padding-bottom: 10px; font-family: Arial, sans-serif""><![endif]-->
<div style=""color:#555555;font-family:Cabin, Arial, Helvetica Neue, Helvetica, sans-serif;line-height:1.2;padding-top:20px;padding-right:10px;padding-bottom:10px;padding-left:10px;"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; line-height: 1.2; color: #555555; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; mso-line-height-alt: 14px;"">
<p style=""margin: 0; font-size: 16px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 19px; margin-top: 0; margin-bottom: 0;""><span style=""font-size: 16px;"">Thanks so much for joining our site!</span></p>
</div>
</div>
<!--[if mso]></td></tr></table><![endif]-->
<!--[if (!mso)&(!IE)]><!-->
</div>
<!--<![endif]-->
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
</div>
</div>
</div>
<div style=""background-image:url('https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&amp;inputFormat=png&amp;cs=fFNQTw&amp;docid=https%3A%2F%2Fvitesco-my.sharepoint.com%3A443%2F_api%2Fv2.0%2Fdrives%2Fb!mMdYw1SQ-0erpy8hbET0tIalLhKYWZhAp8hT60i5bsiRLizQxH2MSLkH0GVZ0gJd%2Fitems%2F01K7VQC6KUDLJBHE5D5BAIHAZ7RRRAU5I4%3Fversion%3DPublished&amp;access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvdml0ZXNjby1teS5zaGFyZXBvaW50LmNvbUAzOWI3NzEwMS05OWI3LTQxYzktOGQ2YS03Nzk0YjlkNDg0NzYiLCJpc3MiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAiLCJuYmYiOiIxNjIzMzE1NjAwIiwiZXhwIjoiMTYyMzMzNzIwMCIsImVuZHBvaW50dXJsIjoiZVU3eVhqdTAwUjJvcnoyOGNxRlN1UXdWQm9KMWRmdWh1ejJVU2lZclhoYz0iLCJlbmRwb2ludHVybExlbmd0aCI6IjExNyIsImlzbG9vcGJhY2siOiJUcnVlIiwidmVyIjoiaGFzaGVkcHJvb2Z0b2tlbiIsInNpdGVpZCI6Ill6TTFPR00zT1RndE9UQTFOQzAwTjJaaUxXRmlZVGN0TW1ZeU1UWmpORFJtTkdJMCIsInNpZ25pbl9zdGF0ZSI6IltcImttc2lcIixcImR2Y19kbWpkXCJdIiwibmFtZWlkIjoiMCMuZnxtZW1iZXJzaGlwfHVpZGo3ODgyQHZpdGVzY28uY29tIiwibmlpIjoibWljcm9zb2Z0LnNoYXJlcG9pbnQiLCJpc3VzZXIiOiJ0cnVlIiwiY2FjaGVrZXkiOiIwaC5mfG1lbWJlcnNoaXB8MTAwMzIwMDBkNTZmNjhjZkBsaXZlLmNvbSIsInR0IjoiMCIsInVzZVBlcnNpc3RlbnRDb29raWUiOiIzIn0.WXVLWEExWG83cklxMG5kUWdaN2JCNnRtY2ZDQi9PSWZIMm5rS1dlZTE1RT0&amp;encodeFailures=1&amp;width=1920&amp;height=956&amp;srcWidth=&amp;srcHeight=');background-position:top center;background-repeat:no-repeat;background-color:transparent;"">
<div class=""block-grid no-stack"" style=""min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; Margin: 0 auto; background-color: transparent;"">
<div style=""border-collapse: collapse;display: table;width: 100%;background-color:transparent;"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-image:url('https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&amp;inputFormat=png&amp;cs=fFNQTw&amp;docid=https%3A%2F%2Fvitesco-my.sharepoint.com%3A443%2F_api%2Fv2.0%2Fdrives%2Fb!mMdYw1SQ-0erpy8hbET0tIalLhKYWZhAp8hT60i5bsiRLizQxH2MSLkH0GVZ0gJd%2Fitems%2F01K7VQC6KUDLJBHE5D5BAIHAZ7RRRAU5I4%3Fversion%3DPublished&amp;access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvdml0ZXNjby1teS5zaGFyZXBvaW50LmNvbUAzOWI3NzEwMS05OWI3LTQxYzktOGQ2YS03Nzk0YjlkNDg0NzYiLCJpc3MiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAiLCJuYmYiOiIxNjIzMzE1NjAwIiwiZXhwIjoiMTYyMzMzNzIwMCIsImVuZHBvaW50dXJsIjoiZVU3eVhqdTAwUjJvcnoyOGNxRlN1UXdWQm9KMWRmdWh1ejJVU2lZclhoYz0iLCJlbmRwb2ludHVybExlbmd0aCI6IjExNyIsImlzbG9vcGJhY2siOiJUcnVlIiwidmVyIjoiaGFzaGVkcHJvb2Z0b2tlbiIsInNpdGVpZCI6Ill6TTFPR00zT1RndE9UQTFOQzAwTjJaaUxXRmlZVGN0TW1ZeU1UWmpORFJtTkdJMCIsInNpZ25pbl9zdGF0ZSI6IltcImttc2lcIixcImR2Y19kbWpkXCJdIiwibmFtZWlkIjoiMCMuZnxtZW1iZXJzaGlwfHVpZGo3ODgyQHZpdGVzY28uY29tIiwibmlpIjoibWljcm9zb2Z0LnNoYXJlcG9pbnQiLCJpc3VzZXIiOiJ0cnVlIiwiY2FjaGVrZXkiOiIwaC5mfG1lbWJlcnNoaXB8MTAwMzIwMDBkNTZmNjhjZkBsaXZlLmNvbSIsInR0IjoiMCIsInVzZVBlcnNpc3RlbnRDb29raWUiOiIzIn0.WXVLWEExWG83cklxMG5kUWdaN2JCNnRtY2ZDQi9PSWZIMm5rS1dlZTE1RT0&amp;encodeFailures=1&amp;width=1920&amp;height=956&amp;srcWidth=&amp;srcHeight=');background-position:top center;background-repeat:no-repeat;background-color:transparent;""><tr><td align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:600px""><tr class=""layout-full-width"" style=""background-color:transparent""><![endif]-->
<!--[if (mso)|(IE)]><td align=""center"" width=""600"" style=""background-color:transparent;width:600px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;"" valign=""top""><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 35px; padding-left: 35px; padding-top:15px; padding-bottom:2px;""><![endif]-->
<div class=""col num12"" style=""min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top; width: 600px;"">
<div class=""col_cont"" style=""width:100% !important;"">
<!--[if (!mso)&(!IE)]><!-->
<div style=""border-top:0px solid transparent; border-left:1px solid #F0E614; border-bottom:0px solid transparent; border-right:1px solid #F0E614; padding-top:15px; padding-bottom:2px; padding-right: 35px; padding-left: 35px;"">
<!--<![endif]-->
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 10px; padding-left: 10px; padding-top: 15px; padding-bottom: 15px; font-family: Arial, sans-serif""><![endif]-->
<div style=""color:#555555;font-family:Cabin, Arial, Helvetica Neue, Helvetica, sans-serif;line-height:1.2;padding-top:15px;padding-right:10px;padding-bottom:15px;padding-left:10px;"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; line-height: 1.2; color: #555555; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; mso-line-height-alt: 14px;"">
<p style=""margin: 0; font-size: 16px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 19px; margin-top: 0; margin-bottom: 0;""><span style=""font-size: 16px;"">To finish signing up and <span style=""color: #132f40; font-size: 16px;""><strong>activate your account </strong></span></span></p>
<p style=""margin: 0; font-size: 16px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 19px; margin-top: 0; margin-bottom: 0;""><span style=""font-size: 16px;"">you just need to click bellow.</span></p>
</div>
</div>
<!--[if mso]></td></tr></table><![endif]-->
<div align=""left"" class=""button-container"" style=""padding-top:5px;padding-right:10px;padding-bottom:35px;padding-left:10px;"">
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;""><tr><td style=""padding-top: 5px; padding-right: 10px; padding-bottom: 35px; padding-left: 10px"" align=""left""><v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""";


public string confirmPageP2 = @""" style=""height:31.5pt;width:412.5pt;v-text-anchor:middle;"" arcsize=""120%"" stroke=""false"" fillcolor=""#FFD500""><w:anchorlock/><v:textbox inset=""0,0,0,0""><center style=""color:#132F40; font-family:Arial, sans-serif; font-size:15px""><![endif]--><a href=""";

public string confirmPageP3 = @""" style=""-webkit-text-size-adjust: none; text-decoration: none; display: block; color: #132F40; background-color: #FFC300; border-radius: 8px; -webkit-border-radius: 50px; -moz-border-radius: 8px; width: 100%; width: calc(100% - 2px); border-top: 1px solid #FFC300; border-right: 1px solid #FFC300; border-bottom: 1px solid #FFC300; border-left: 1px solid #FFC300; padding-top: 5px; padding-bottom: 5px; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;"" target=""_blank""><span style=""padding-left:20px;padding-right:20px;font-size:15px;display:inline-block;letter-spacing:undefined;""><span style=""font-size: 16px; line-height: 2; word-break: break-word; mso-line-height-alt: 32px;""><span data-mce-style=""font-size: 15px; line-height: 30px;"" style=""font-size: 15px; line-height: 30px;""><strong><span data-mce-style=""line-height: 30px; font-size: 15px;"" style=""line-height: 30px; font-size: 15px;"">ACTIVATE MY ACCOUNT &gt;</span></strong></span></span></span></a>
<!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->
</div>
<!--[if (!mso)&(!IE)]><!-->
</div>
<!--<![endif]-->
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
</div>
</div>
</div>
<div style=""background-color:transparent;"">
<div class=""block-grid no-stack"" style=""min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; Margin: 0 auto; background-color: #132f40;"">
<div style=""border-collapse: collapse;display: table;width: 100%;background-color:#132f40;"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:transparent;""><tr><td align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:600px""><tr class=""layout-full-width"" style=""background-color:#132f40""><![endif]-->
<!--[if (mso)|(IE)]><td align=""center"" width=""600"" style=""background-color:#132f40;width:600px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;"" valign=""top""><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 0px; padding-left: 25px; padding-top:15px; padding-bottom:15px;""><![endif]-->
<div class=""col num12"" style=""min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top; width: 600px;"">
<div class=""col_cont"" style=""width:100% !important;"">
<!--[if (!mso)&(!IE)]><!-->
<div style=""border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:15px; padding-bottom:15px; padding-right: 0px; padding-left: 25px;"">
<!--<![endif]-->
<!--[if mso]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 10px; padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-family: Arial, sans-serif""><![endif]-->
<div style=""color:#F8F8F8;font-family:Cabin, Arial, Helvetica Neue, Helvetica, sans-serif;line-height:1.2;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; line-height: 1.2; color: #F8F8F8; font-family: Cabin, Arial, Helvetica Neue, Helvetica, sans-serif; mso-line-height-alt: 14px;"">
<p style=""margin: 0; font-size: 14px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 17px; margin-top: 0; margin-bottom: 0;""><strong>Machine Portal</strong></p>
<p style=""margin: 0; font-size: 14px; line-height: 1.2; word-break: break-word; mso-line-height-alt: 17px; margin-top: 0; margin-bottom: 0;"">Salto Plant - <strong>Brazil</strong></p>
</div>
</div>
<!--[if mso]></td></tr></table><![endif]-->
<!--[if (!mso)&(!IE)]><!-->
</div>
<!--<![endif]-->
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
</div>
</div>
</div>
<div style=""background-color:transparent;"">
<div class=""block-grid"" style=""min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; Margin: 0 auto; background-color: transparent;"">
<div style=""border-collapse: collapse;display: table;width: 100%;background-color:transparent;"">
<!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:transparent;""><tr><td align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:600px""><tr class=""layout-full-width"" style=""background-color:transparent""><![endif]-->
<!--[if (mso)|(IE)]><td align=""center"" width=""600"" style=""background-color:transparent;width:600px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;"" valign=""top""><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;""><![endif]-->
<div class=""col num12"" style=""min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top; width: 600px;"">
<div class=""col_cont"" style=""width:100% !important;"">
<!--[if (!mso)&(!IE)]><!-->
<div style=""border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;"">
<!--<![endif]-->
<table cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" valign=""top"" width=""100%"">
<tr style=""vertical-align: top;"" valign=""top"">
<td align=""center"" style=""word-break: break-word; vertical-align: top; padding-top: 5px; padding-right: 0px; padding-bottom: 5px; padding-left: 0px; text-align: center;"" valign=""top"">
<!--[if vml]><table align=""left"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;""><![endif]-->
<!--[if !vml]><!-->
</td>
</tr>
</table>
<!--[if (!mso)&(!IE)]><!-->
</div>
<!--<![endif]-->
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
<!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]-->
</div>
</div>
</div>
<!--[if (mso)|(IE)]></td></tr></table><![endif]-->
</td>
</tr>
</tbody>
</table>
<!--[if (IE)]></div><![endif]-->
</body>
</html>";

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Mobile Number")]
            public string Mobile { get; set; }

            [Display(Name = "Department")]
            public string Department { get; set; }

            [Display(Name = "Job Role")]
            public string JobRole { get; set; }

            [Display(Name = "Photo")]
            public string PhotoPath { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile photo, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var dpt = _context.Department.FirstOrDefault(d => d.Name == "User");
                var user = new MachinePortalUser { UserName = Input.UserName,
                                                    FirstName = Input.FirstName,
                                                    LastName = Input.LastName,
                                                    Email = Input.Email,
                                                    EmailConfirmed = false,
                                                    Department = dpt,
                                                    JobRole = Input.JobRole,
                                                    Mobile = Input.Mobile,
                                                    PhoneNumber = Input.PhoneNumber,
                                                    PhotoPath = Input.PhotoPath,
                                                  };

                if (photo != null && photo.Length > 0)
                {
                    //long filesSize = photo.Length;
                    //var filePath = Path.GetTempFileName();

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    fileName += photo.FileName.Substring(photo.FileName.LastIndexOf("."), (photo.FileName.Length - photo.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Users\\Photos\\" + fileName;
                    user.PhotoPath = @"/resources/Users/Photos/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    MachinePortalUser defaultUser = _context.Users.Include(up => up.UserPermissions).ThenInclude(p => p.Permission).FirstOrDefault(u => u.UserName == "user");

                    foreach (Permission p in defaultUser.UserPermissions.Select(x => x.Permission).ToList())
                    {
                        UserPermission UP = new UserPermission { UserID = user.Id, Permission = p, PermissionID = p.ID, MachinePortalUser = user };
                        await _context.UserPermission.AddAsync(UP);
                    }
                    await _context.SaveChangesAsync();

                    MachinePortalUser newUser = _context.Users.Include(up => up.UserPermissions).ThenInclude(p => p.Permission).FirstOrDefault(u => u.Id == user.Id);

                    try
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code },
                            protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _emailSender.SendEmailAsync(Input.Email, "Machine Portal - Email Confirmation", confirmPageP1 + HtmlEncoder.Default.Encode(callbackUrl) + confirmPageP2 + HtmlEncoder.Default.Encode(callbackUrl) + confirmPageP3);

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        returnUrl = "/Identity/Account/WaitingConfirmation";
                    }
                    catch(Exception e)
                    {
                        ModelState.AddModelError(string.Empty, e.Message);
                        returnUrl = "/";
                    }
                    
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
