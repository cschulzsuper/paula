﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Super.Paula
{
    public class TypeNameConverterTests
    {
        [Theory]
        [InlineData(typeof(TypeNameConverterTests), "type-name-converter-tests")]
        [InlineData(typeof(UInt32), "u-int-32")]
        public void ToKebabCase_ReturnsKebabCase_ForTypeNameConverterTests(Type type, string kebabCase)
        {
            // Act
            var result = TypeNameConverter.ToKebabCase(type);

            // Assert
            result.Should().Be(kebabCase);
        }
    }
}
