﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System.Linq;
using Xunit;

namespace Microsoft.Restier.Core.Tests
{
    public class InvocationContextTests
    {
        [Fact]
        public void NewInvocationContextIsConfiguredCorrectly()
        {
            var configuration = new DomainConfiguration();
            configuration.EnsureCommitted();
            var domainContext = new DomainContext(configuration);
            var context = new InvocationContext(domainContext);
            Assert.Same(domainContext, context.DomainContext);
        }

        [Fact]
        public void InvocationContextGetsHookPointsCorrectly()
        {
            var configuration = new DomainConfiguration();
            var singletonHookPoint = new object();
            configuration.SetHookPoint(typeof(object), singletonHookPoint);
            var multiCastHookPoint = new object();
            configuration.AddHookPoint(typeof(object), multiCastHookPoint);
            configuration.EnsureCommitted();

            var domainContext = new DomainContext(configuration);
            var context = new InvocationContext(domainContext);

            Assert.Same(singletonHookPoint, context.GetHookPoint<object>());
            Assert.True(context.GetHookPoints<object>()
                .SequenceEqual(new object[] { multiCastHookPoint }));
        }
    }
}
