namespace ReQuest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTables : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Buildings ON");
            Sql("INSERT INTO Buildings (BuildingId, BuildingLetter, BuildingDepartment) VALUES (1, 'F', 'Communication')");
            Sql("INSERT INTO Buildings (BuildingId, BuildingLetter, BuildingDepartment) VALUES (2, 'G', 'Engineering')");
            Sql("INSERT INTO Buildings (BuildingId, BuildingLetter, BuildingDepartment) VALUES (3, 'E', 'Humanities')");
            Sql("SET IDENTITY_INSERT Buildings OFF");


            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('G149', 30, 2)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('G240', 30, 2)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('G247', 20, 2)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('F242', 30, 1)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('F215', 25, 1)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('E149', 30, 3)");
            Sql("INSERT INTO Rooms (RoomNumber, RoomCapacity, BuildingID) VALUES ('E247', 30, 3)");


            Sql("SET IDENTITY_INSERT Materials ON");
            Sql("INSERT INTO Materials (MaterialId, MaterialName, MaterialDescription) VALUES (1, N'Board', N'Marker Board')");
            Sql("INSERT INTO Materials (MaterialId, MaterialName, MaterialDescription) VALUES (2, N'Computers', N'OS: Linux and Windows')");
            Sql("INSERT INTO Materials (MaterialId, MaterialName, MaterialDescription) VALUES (3, N'Television', N'Digital Television with HDMI')");
            Sql("SET IDENTITY_INSERT Materials OFF");
        }
        
        public override void Down()
        {
        }
    }
}
