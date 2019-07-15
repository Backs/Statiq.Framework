﻿using System;
using NUnit.Framework;
using Shouldly;
using Statiq.Common;
using Statiq.Testing;

namespace Statiq.Core.Tests.Shortcodes
{
    [TestFixture]
    public class ShortcodeCollectionFixture : BaseFixture
    {
        public class CreateInstanceTests : ShortcodeCollectionFixture
        {
            [Test]
            public void CreatesInstance()
            {
                // Given
                ShortcodeCollection shortcodes = new ShortcodeCollection();
                shortcodes.Add<TestShortcode>("Foo");

                // When
                IShortcode shortcode = shortcodes.CreateInstance("Foo");

                // Then
                shortcode.ShouldBeAssignableTo<TestShortcode>();
            }

            [Test]
            public void IgnoresCase()
            {
                // Given
                ShortcodeCollection shortcodes = new ShortcodeCollection();
                shortcodes.Add<TestShortcode>("Foo");

                // When
                IShortcode shortcode = shortcodes.CreateInstance("foo");

                // Then
                shortcode.ShouldBeAssignableTo<TestShortcode>();
            }
        }

        public class AddTests : ShortcodeCollectionFixture
        {
            [Test]
            public void ThrowsForNullName()
            {
                // Given
                ShortcodeCollection shortcodes = new ShortcodeCollection();

                // When, Then
                Should.Throw<ArgumentException>(() => shortcodes.Add<TestShortcode>(null));
            }

            [Test]
            public void ThrowsForWhiteSpaceName()
            {
                // Given
                ShortcodeCollection shortcodes = new ShortcodeCollection();

                // When, Then
                Should.Throw<ArgumentException>(() => shortcodes.Add<TestShortcode>("  "));
            }

            [Test]
            public void ThrowsForWhiteSpaceInName()
            {
                // Given
                ShortcodeCollection shortcodes = new ShortcodeCollection();

                // When, Then
                Should.Throw<ArgumentException>(() => shortcodes.Add<TestShortcode>("  xyz  "));
            }
        }
    }
}
