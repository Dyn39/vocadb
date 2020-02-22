using FluentNHibernate.Mapping;
using VocaDb.Model.Domain.Venues;

namespace VocaDb.Model.Mapping.Venues {

	public class VenueMap : ClassMap<Venue> {

		public VenueMap() {

			Table("Venues");
			Cache.ReadWrite();
			Id(m => m.Id);

			Map(m => m.Deleted).Not.Nullable();
			Map(m => m.Description).Length(1000).Not.Nullable();
			Map(m => m.Status).Not.Nullable();
			Map(m => m.Version).Not.Nullable();

			Component(m => m.ArchivedVersionsManager,
				c => c.HasMany(m => m.Versions).KeyColumn("Venue").Inverse().Cascade.All().OrderBy("Created DESC"));

			Component(m => m.Names, c => {
				c.Map(m => m.AdditionalNamesString).Not.Nullable().Length(1024);
				c.HasMany(m => m.Names).Table("VenueNames").KeyColumn("Venue").Inverse().Cascade.All().Cache.ReadWrite();
				c.Component(m => m.SortNames, c2 => {
					c2.Map(m => m.DefaultLanguage, "DefaultNameLanguage");
					c2.Map(m => m.Japanese, "JapaneseName");
					c2.Map(m => m.English, "EnglishName");
					c2.Map(m => m.Romaji, "RomajiName");
				});
			});

			HasMany(m => m.WebLinks).KeyColumn("Venue").Inverse().Cascade.All().Cache.ReadWrite();

		}

	}

	public class VenueNameMap : ClassMap<VenueName> {

		public VenueNameMap() {

			Table("VenueNames");
			Cache.ReadWrite();
			Id(m => m.Id);

			Map(m => m.Language).Not.Nullable();
			Map(m => m.Value).Length(255).Not.Nullable();
			References(m => m.Entry).Column("Venue").Not.Nullable();

		}

	}

	public class VenueWebLinkMap : WebLinkMap<VenueWebLink, Venue> { }

	public class ArchivedVenueVersionMap : ClassMap<ArchivedVenueVersion> {

		public ArchivedVenueVersionMap() {

			Table("ArchivedVenueVersions");
			Id(m => m.Id);

			Map(m => m.CommonEditEvent).Length(30).Not.Nullable();
			Map(m => m.Created).Not.Nullable();
			Map(m => m.Data);
			Map(m => m.Notes).Length(200).Not.Nullable();
			Map(m => m.Version).Not.Nullable();

			References(m => m.Author).Not.Nullable();
			References(m => m.Entry).Column("Venue").Not.Nullable();

			Component(m => m.Diff, c => {
				c.Map(m => m.ChangedFieldsString, "ChangedFields").Length(100).Not.Nullable();
			});

		}

	}

}
