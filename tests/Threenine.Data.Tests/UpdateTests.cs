/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using TestDatabase;
using Threenine.Data.Tests.TestFixtures;
using Xunit;

namespace Threenine.Data.Tests
{
    [Collection(GlobalTestStrings.ProductCollectionName)]
    public class UpdateTests : IDisposable
    {
        public UpdateTests(SqlLiteWith20ProductsTestFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }

        private readonly SqlLiteWith20ProductsTestFixture _fixture;

        [Fact]
        public void ShouldThrowInvalidOperationException()
        {
            const string newProductName = "Foo Bar";
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product = repo.GetSingleOrDefault(x => x.Id == 1, enableTracking: false);

            Assert.IsAssignableFrom<TestProduct>(product);

            product.Name = newProductName;

            Assert.Throws<InvalidOperationException>(() => repo.Update(product));
        }

        [Fact]
        public void ShouldUpdateMultipleProductsByParams()
        {
            const string newProduct1Name = "Foo Bar";
            const string newProduct2Name = "Bar Foo";

            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product1 = repo.GetSingleOrDefault(x => x.Id == 1);
            var product2 = repo.GetSingleOrDefault(x => x.Id == 2);

            product1.Name = newProduct1Name;
            product2.Name = newProduct2Name;

            repo.Update(product1, product2);

            uow.Commit();

            var updatedProduct1 = repo.GetSingleOrDefault(x => x.Id == 1);
            var updatedProduct2 = repo.GetSingleOrDefault(x => x.Id == 2);
            Assert.Equal(updatedProduct1.Name, newProduct1Name);
            Assert.Equal(updatedProduct2.Name, newProduct2Name);
        }

        [Fact]
        public void ShouldUpdateProductName()
        {
            const string newProductName = "Foo Bar";
            using var uow = new UnitOfWork<TestDbContext>(_fixture.Context);
            var repo = uow.GetRepository<TestProduct>();

            var product = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.IsAssignableFrom<TestProduct>(product);

            product.Name = newProductName;

            repo.Update(product);

            uow.Commit();

            var updatedProduct = repo.GetSingleOrDefault(x => x.Id == 1);

            Assert.Equal(updatedProduct.Name, newProductName);
        }
    }
}