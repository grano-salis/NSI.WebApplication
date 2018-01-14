using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AddressRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace NSI.Tests
{
    public class AddressRepositoryTest
    {
        private readonly ITestOutputHelper output;

        public AddressRepositoryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void getAddressByIdSuccessTest()
        {
            //var context = new Mock<IkarusContext>();
            //context.Setup(x => x.Add);
            //var addressRepository = new AddressRepository(context.Object);
            //var result = addressRepository.GetAddressById(1);
            //Assert.Null(result);
            
        }

        [Fact]
        public void CreateNewAddressArgumentNullExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            //var address = new AddressDto
            //{
            //    AddressId = id,
            //    Address1 = Address1,
            //    Address2 = Address2,
            //    City = City,
            //    ZipCode = ZipCode,
            //    AddressTypeId = AddressTypeId,
            //    CreatedByUserId = CreatedByUserId,
            //    IsDeleted = isDeleted,
            //    DateCreated = DateCreated,
            //    DateModified = DateModified
            //};

            AddressDto address = null;

            var mockRepo = new Mock<IkarusContext>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.CreateAddress(address);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
            }
            
        }

        [Fact]
        public void CreateNewAddressSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.Add(It.IsAny<Address>()));
            mockRepo.Setup(x => x.SaveChanges()).Returns(1);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.CreateAddress(address);
            Assert.IsType<AddressDto>(result);

        }

        [Fact]
        public void CreateNewAddressExceptionWhileAddingFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.Add(It.IsAny<Address>())).Throws<Exception>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.CreateAddress(address);
            }
            catch (Exception e)
            {
                Assert.IsType<Exception>(e);
            }


        }


        [Fact]
        public void CreateNewAddressReturnsNullFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.Add(It.IsAny<Address>()));
            mockRepo.Setup(x => x.SaveChanges()).Returns(0);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.CreateAddress(address);
            Assert.Null(result);

        }

        [Fact]
        public void GetAddressesSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var data = new List<Address>
            {
                new Address { },
                new Address { },
                new Address { },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.GetAddreses();
            Assert.IsType<List<AddressDto>>(result);

        }

        [Fact]
        public void GetAddressesDataSetNullFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.GetAddreses();
            Assert.Empty(result);

        }

        [Fact]
        public void GetAddressesThrowExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Throws<Exception>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.GetAddreses();
            }
            catch (Exception e)
            {
                Assert.IsType<Exception>(e);
            }

        }

        [Fact]
        public void GetAddressByIdSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var data = new List<Address>
            {
                new Address { AddressId = 0 },
                new Address { },
                new Address { },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.GetAddressById(0);
            Assert.IsType<AddressDto>(result);

        }

        [Fact]
        public void GetAddressesByIdDataSetNullFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var data = new List<Address>
            {

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());


            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.GetAddressById(0);
            Assert.Null(result);

        }

        [Fact]
        public void GetAddressByIdThrowExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Throws<Exception>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.GetAddressById(0);
            }
            catch (Exception e)
            {
                Assert.IsType<Exception>(e);
            }

        }

        [Fact]
        public void DeleteAddressByIdSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };
            var data = new List<Address>
            {
                new Address { AddressId = 0 },
                new Address { },
                new Address { },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.SaveChanges());

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.DeleteAddressById(0);
            Assert.True(result);

        }

        [Fact]
        public void DeleteAddressByIdReturnNullFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };
            var data = new List<Address>
            {

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.SaveChanges());

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.DeleteAddressById(0);
            Assert.False(result);

        }

        [Fact]
        public void DeleteAddressByIdThrowExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };
            var data = new List<Address>
            {
                new Address { AddressId = 0}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.SaveChanges()).Throws<Exception>();

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.DeleteAddressById(0);
            }
            catch(Exception e)
            {
                Assert.IsType<Exception>(e);
            }


        }

        [Fact]
        public void SearchAddressesSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var addressDto = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };
            var mockRepo = new Mock<IkarusContext>();

            var addressRepository = new AddressRepository(mockRepo.Object);

            var result = addressRepository.SearchAddreses(addressDto);

            Assert.Empty(result);

        }

        [Fact]
        public void SearchAddressesThrowExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            AddressDto addressDto = null;
            var mockRepo = new Mock<IkarusContext>();

            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.SearchAddreses(addressDto);
            }
            catch(Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
            }


        }

        [Fact]
        public void EditAddressArgumentNullExceptionFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            //var address = new AddressDto
            //{
            //    AddressId = id,
            //    Address1 = Address1,
            //    Address2 = Address2,
            //    City = City,
            //    ZipCode = ZipCode,
            //    AddressTypeId = AddressTypeId,
            //    CreatedByUserId = CreatedByUserId,
            //    IsDeleted = isDeleted,
            //    DateCreated = DateCreated,
            //    DateModified = DateModified
            //};

            AddressDto address = null;

            var mockRepo = new Mock<IkarusContext>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.EditAddress(0,address);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
            }

        }

        [Fact]
        public void EditAddressSuccessTest()
        {
            int id = 0;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var data = new List<Address>
            {
                new Address { AddressId = 0}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.SaveChanges());

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);
            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.EditAddress(0,address);
            Assert.True(result);

        }

        [Fact]
        public void EditAddressExceptionWhileSaveChangesFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IkarusContext>();
            mockRepo.Setup(x => x.SaveChanges()).Throws<Exception>();
            var addressRepository = new AddressRepository(mockRepo.Object);
            try
            {
                var result = addressRepository.EditAddress(0,address);
            }
            catch (Exception e)
            {
                Assert.IsType<ArgumentNullException>(e);
            }


        }


        [Fact]
        public void EditAddressReturnsFalseFailureTest()
        {
            int id = 0;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var data = new List<Address>
            {

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Address>>();

            mockSet.As<IQueryable<Address>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Address>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Address>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Address>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockRepo = new Mock<IkarusContext>();

            mockRepo.Setup(x => x.Address).Returns(mockSet.Object);

            var addressRepository = new AddressRepository(mockRepo.Object);
            var result = addressRepository.EditAddress(0,address);
            Assert.False(result);

        }
    }
}
