﻿using System;
using System.Linq;
using System.Net.Mail;
using Super.Paula.Validation;

namespace Super.Paula.Application.Administration
{
    public static class InspectorValidator
    {
        public static (Func<bool, bool>, Func<FormattableString>) InspectorHasValue(string inspector)
            => (_ => !string.IsNullOrWhiteSpace(inspector),
                    () => $"Inspector must have a value");

        public static (Func<bool, bool>, Func<FormattableString>) InspectorHasKebabCase(string inspector)
            => (_ => KebabCaseValidator.IsValid(inspector),
                    () => $"Inspector '{inspector}' must be in kebab case");

        public static (Func<bool, bool>, Func<FormattableString>) InspectorExists(string inspector, IQueryable<Inspector> inspectors)
            => (x => !x || inspectors.FirstOrDefault(x => x.UniqueName == inspector) != null,
                    () => $"Inspector '{inspector}' does not exist");

        public static (Func<bool, bool>, Func<FormattableString>) UniqueNameHasValue(Inspector inspector)
            => (_ => !string.IsNullOrWhiteSpace(inspector.UniqueName),
                    () => $"Unique name of inspector must have a value");

        public static (Func<bool, bool>, Func<FormattableString>) UniqueNameHasKebabCase(Inspector inspector)
            => (_ => KebabCaseValidator.IsValid(inspector.UniqueName),
                    () => $"Unique name '{inspector.UniqueName}' of inspector must be in kebab case");

        public static (Func<bool, bool>, Func<FormattableString>) UniqueNameIsUnqiue(Inspector inspector, IQueryable<Inspector> inspectors)
            => (x => !x || (inspectors.FirstOrDefault(x => x.UniqueName == inspector.UniqueName) == null),
                    () => $"Unique name '{inspector.UniqueName}' of inspector already exists");

        public static (Func<bool, bool>, Func<FormattableString>) UniqueNameExists(Inspector inspector, IQueryable<Inspector> inspectors)
            => (x => !x || (inspectors.FirstOrDefault(x => x.UniqueName == inspector.UniqueName) != null),
                    () => $"Unique name '{inspector.UniqueName}' of inspector does not exist");

        public static (Func<bool, bool>, Func<FormattableString>) MailAddressIsNotNull(Inspector inspector)
            => (_ => inspector.MailAddress != null,
                    () => $"Mail address of inspector '{inspector.UniqueName}' can not be null");

        public static (Func<bool, bool>, Func<FormattableString>) MailAddressIsMailAddress(Inspector inspector)
            => (_ => MailAddress.TryCreate(inspector.MailAddress, out var _),
                    () => $"Mail address of inspector '{inspector.UniqueName}' is not a mail address");

    }
}