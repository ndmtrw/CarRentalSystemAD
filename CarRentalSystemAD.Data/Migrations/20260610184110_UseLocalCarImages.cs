using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystemAD.Data.Migrations
{
    /// <inheritdoc />
    public partial class UseLocalCarImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/cars/toyota-corolla.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/cars/bmw-x5.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/cars/mercedes-s-class.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/cars/vw-touran.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/2019_Toyota_Corolla_sedan_%28facelift%2C_red%29%2C_front_8.15.19.jpg/1280px-2019_Toyota_Corolla_sedan_%28facelift%2C_red%29%2C_front_8.15.19.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/2019_BMW_X5_xDrive40i_in_Phytonic_Blue%2C_front_8.6.19.jpg/1280px-2019_BMW_X5_xDrive40i_in_Phytonic_Blue%2C_front_8.6.19.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/2021_Mercedes-Benz_S-Class_%28W223%29_AMG_Line_saloon_%282021-11-01%29_01.jpg/1280px-2021_Mercedes-Benz_S-Class_%28W223%29_AMG_Line_saloon_%282021-11-01%29_01.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/VW_Touran_1.5_TSI_EVO_Comfortline_%28III%29_%E2%80%93_Frontansicht%2C_3._September_2019%2C_D%C3%BCsseldorf.jpg/1280px-VW_Touran_1.5_TSI_EVO_Comfortline_%28III%29_%E2%80%93_Frontansicht%2C_3._September_2019%2C_D%C3%BCsseldorf.jpg");
        }
    }
}
