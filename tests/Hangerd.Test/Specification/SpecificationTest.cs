namespace Hangerd.Test.Specification
{
	using Hangerd.Specification;
	using NUnit.Framework;
	using System.Collections.Generic;
	using System.Linq;

	public class SpecificationTests
	{
		readonly static string _EntityName = "Test Entity Name";
		readonly static string _OtherEntityName = "Other Test Entity Name";

		readonly static List<SampleEntity> _EntityList = new List<SampleEntity>
			{
				new SampleEntity() { Name = _EntityName, Order = 1 },
				new SampleEntity() { Name = _EntityName, Order = 10 },
				new SampleEntity() { Name = _OtherEntityName, Order = 100 }
			};

		[Test]
		public void CreateAndSpecificationTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _EntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			var composite = new AndSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

			Assert.IsNotNull(composite.SatisfiedBy());
			Assert.AreEqual(leftAdHocSpecification, composite.LeftSideSpecification);
			Assert.AreEqual(rightAdHocSpecification, composite.RightSideSpecification);

			var result = _EntityList.AsQueryable().Where(composite.SatisfiedBy()).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void CreateOrSpecificationTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _OtherEntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			var composite = new OrSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

			Assert.IsNotNull(composite.SatisfiedBy());
			Assert.AreEqual(leftAdHocSpecification, composite.LeftSideSpecification);
			Assert.AreEqual(rightAdHocSpecification, composite.RightSideSpecification);

			var result = _EntityList.AsQueryable().Where(composite.SatisfiedBy()).ToList();

			Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void UseSpecificationLogicAndOperatorTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _EntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			ISpecification<SampleEntity> andSpec = leftAdHocSpecification & rightAdHocSpecification;

			var result = _EntityList.AsQueryable().Where(andSpec.SatisfiedBy()).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void UseSpecificationAndOperatorTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _EntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			ISpecification<SampleEntity> andSpec = leftAdHocSpecification && rightAdHocSpecification;

			var result = _EntityList.AsQueryable().Where(andSpec.SatisfiedBy()).ToList();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void UseSpecificationBitwiseOrOperatorTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _OtherEntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			ISpecification<SampleEntity> orSpec = leftAdHocSpecification | rightAdHocSpecification;

			var result = _EntityList.AsQueryable().Where(orSpec.SatisfiedBy()).ToList();

			Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void UseSpecificationOrOperatorTest()
		{
			var leftAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Name == _OtherEntityName);
			var rightAdHocSpecification = new DirectSpecification<SampleEntity>(s => s.Order > 1);

			ISpecification<SampleEntity> orSpec = leftAdHocSpecification || rightAdHocSpecification;

			var result = _EntityList.AsQueryable().Where(orSpec.SatisfiedBy()).ToList();

			Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void CreateNotSpecificationFromNegationOperator()
		{
			var spec = new DirectSpecification<SampleEntity>(s=>s.Name==_EntityName);

			ISpecification<SampleEntity> notSpec = !spec;

			Assert.IsNotNull(notSpec);
		}

		[Test]
		public void CheckNotSpecificationOperators()
		{
			var spec = new DirectSpecification<SampleEntity>(s=>s.Name==_EntityName);

			var notSpec = !spec;

			ISpecification<SampleEntity> resultAnd = notSpec && spec;
			ISpecification<SampleEntity> resultOr = notSpec || spec;

			Assert.IsNotNull(notSpec);
			Assert.IsNotNull(resultAnd);
			Assert.IsNotNull(resultOr);
		}
	
		[Test]
		public void CreateTrueSpecificationTest()
		{
			ISpecification<SampleEntity> trueSpec = new TrueSpecification<SampleEntity>();

			Assert.IsNotNull(trueSpec);

			var result = trueSpec.SatisfiedBy().Compile()(new SampleEntity());

			Assert.AreEqual(true, result);
		}
	}
}
