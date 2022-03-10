﻿using Microsoft.Playwright;
using Super.Paula.Environment;
using Super.Paula.Templates.Playwright.Administration;
using Super.Paula.Templates.Playwright.Auditing;
using Super.Paula.Templates.Playwright.Guidelines;
using Super.Paula.Templates.Playwright.Inventory;
using System.Threading.Tasks;

using playwright = Microsoft.Playwright;

namespace Super.Paula.Templates.AdventureTours.Steps
{
    public class InitializationAdventureTours : IStep
    {
        private readonly AppSettings _appSettings;

        public InitializationAdventureTours(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task ExecuteAsync()
        {
            using var playwright = await playwright::Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                BaseURL = _appSettings.Client
            });

            var page = await context.NewPageAsync();

            var adventureToursMaintainerIdentity = "bknightingale";
            var adventureToursMaintainerPassword = "barnabus";

            var adventureToursDemoIdentity = _appSettings.DemoIdentity;
            var adventureToursDemoPassword = _appSettings.DemoPassword;
            var adventureToursDemoInspector = _appSettings.DemoIdentity;

            await page.SignInAsync(
                adventureToursMaintainerIdentity,
                adventureToursMaintainerPassword,
                withAutomaticInspectorLogin: true);

            await page.CreateBusinessObjectAsync("Radio #1234", "radio-1234", adventureToursDemoInspector);
            await page.CreateBusinessObjectAsync("Radio #1424", "radio-1424", adventureToursDemoInspector);
            await page.CreateBusinessObjectAsync("GPS Tracker #2234", "gps-tracker-2234", adventureToursDemoInspector);
            await page.CreateBusinessObjectAsync("GPS Tracker #2424", "gps-tracker-2424", adventureToursDemoInspector);
            await page.CreateBusinessObjectAsync("Flashlight #134", "flashlight-134", adventureToursDemoInspector);
            await page.CreateBusinessObjectAsync("Flashlight #142", "flashlight-142", adventureToursDemoInspector);

            await page.CreateInspectionAsync("Charged", "charged");
            await page.CreateInspectionAsync("Operational", "operational");
            await page.CreateInspectionAsync("Condition", "condition");

            await page.CreateBusinessObjectInspectionAsync("radio-1234", "charged");
            await page.DeleteBusinessObjectInspectionAsync("radio-1234", "charged");

            await page.CreateBusinessObjectInspectionAsync("radio-1234", "charged");
            await page.CreateBusinessObjectInspectionAsync("radio-1234", "operational");
            await page.CreateBusinessObjectInspectionAsync("radio-1234", "condition");

            await page.CreateBusinessObjectInspectionAsync("radio-1424", "charged");
            await page.CreateBusinessObjectInspectionAsync("radio-1424", "operational");
            await page.CreateBusinessObjectInspectionAsync("radio-1424", "condition");

            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2234", "charged");
            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2234", "operational");
            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2234", "condition");

            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2424", "charged");
            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2424", "operational");
            await page.CreateBusinessObjectInspectionAsync("gps-tracker-2424", "condition");

            await page.CreateBusinessObjectInspectionAsync("flashlight-134", "charged");
            await page.CreateBusinessObjectInspectionAsync("flashlight-134", "operational");
            await page.CreateBusinessObjectInspectionAsync("flashlight-134", "condition");

            await page.CreateBusinessObjectInspectionAsync("flashlight-142", "charged");
            await page.CreateBusinessObjectInspectionAsync("flashlight-142", "operational");
            await page.CreateBusinessObjectInspectionAsync("flashlight-142", "condition");

            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1234", "charged", "30 12 * * 1", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1234", "operational", "35 12 * * 1", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1234", "condition", "40 12 * * 1", "08:00:00");

            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1424", "charged", "25 11 * * 2", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1424", "operational", "30 11 * * 2", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("radio-1424", "condition", "35 11 * * 2", "08:00:00");

            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2234", "charged", "30 15 * * 3", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2234", "operational", "40 15 * * 3", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2234", "condition", "50 15 * * 3", "08:00:00");

            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2424", "charged", "15 8 * * 4", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2424", "operational", "30 8 * * 4", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("gps-tracker-2424", "condition", "45 8 * * 4", "08:00:00");

            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-134", "charged", "10 12 * * 5", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-134", "operational", "15 12 * * 5", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-134", "condition", "20 12 * * 5", "08:00:00");

            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-142", "charged", "45 13 * * 6", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-142", "operational", "50 13 * * 6", "08:00:00");
            await page.ScheduleBusinessObjectInspectionAuditAsync("flashlight-142", "condition", "55 13 * * 6", "08:00:00");

            await page.OmitBusinessObjectInspectionAuditAsync("radio-1234", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("radio-1234", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("radio-1234", "condition");

            await page.OmitBusinessObjectInspectionAuditAsync("radio-1424", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("radio-1424", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("radio-1424", "condition");

            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2234", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2234", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2234", "condition");

            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2424", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2424", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("gps-tracker-2424", "condition");

            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-134", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-134", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-134", "condition");

            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-142", "charged");
            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-142", "operational");
            await page.OmitBusinessObjectInspectionAuditAsync("flashlight-142", "condition");

            await page.SignOutAsync();

            await page.SignInAsync(
                adventureToursDemoIdentity,
                adventureToursDemoPassword,
                withAutomaticInspectorLogin: true);

            await page.AuditBusinessObjectInspectionAsync("radio-1234", "charged");
            await page.AuditBusinessObjectInspectionAsync("radio-1234", "operational");
            await page.AuditBusinessObjectInspectionAsync("radio-1234", "condition");

            await page.AuditBusinessObjectInspectionAsync("radio-1424", "charged");
            await page.AuditBusinessObjectInspectionAsync("radio-1424", "operational");
            await page.AuditBusinessObjectInspectionAsync("radio-1424", "condition");

            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2234", "charged");
            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2234", "operational");
            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2234", "condition");

            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2424", "charged");
            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2424", "operational");
            await page.AuditBusinessObjectInspectionAsync("gps-tracker-2424", "condition");

            await page.AuditBusinessObjectInspectionAsync("flashlight-134", "charged");
            await page.AuditBusinessObjectInspectionAsync("flashlight-134", "operational");
            await page.AuditBusinessObjectInspectionAsync("flashlight-134", "condition");

            await page.AuditBusinessObjectInspectionAsync("flashlight-142", "charged");
            await page.AuditBusinessObjectInspectionAsync("flashlight-142", "operational");
            await page.AuditBusinessObjectInspectionAsync("flashlight-142", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1234", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1234", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1234", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1424", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1424", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("radio-1424", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2234", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2234", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2234", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2424", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2424", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("gps-tracker-2424", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-134", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-134", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-134", "condition");

            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-142", "charged");
            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-142", "operational");
            await page.AnnotateBusinessObjectInspectionAuditAsync("flashlight-142", "condition");

            await page.SignOutAsync();
        }
    }
}