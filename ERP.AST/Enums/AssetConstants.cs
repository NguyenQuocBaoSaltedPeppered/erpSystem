namespace ERP.AST.Enum
{
    public class AssetConstants
    {
        public static string TEMPLATE_IMPORT_PATH = "/public/template/export/";

        public static string IMPORT_ASSET_TEMPLATE_FILE_NAME = "S001_TemplateCreateAsset.xlsx";

        public static int PAGE_SIZE_DEFAULT_FIFTY = 50;

        public static string[] FORMAT_DATE_EXCEL = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss K", "yyyy-MM-dd HH:mm:ss" };

        #region prefix
        public static readonly string ASSET = "TS";

        public static readonly string INCREASE_CODE = "BT";

        public static readonly string RECALL_CODE = "TH";

        public static readonly string ALLOCATION_CODE = "CP";

        public static readonly string BROKEN_CODE = "BH";

        public static readonly string GUARANTEE_CODE = "BHSC";

        public static readonly string CANCELLATION_CODE = "BHY";

        public static readonly string LOST_CODE = "BM";

        public static readonly string ASSET_TRANSFER = "BGTS";

        public static readonly string ASSET_HAND_OVER_IMPORT = "NKBG";

        public static readonly string ASSET_HAND_OVER_EXPORT = "XKBG";

        public static readonly string ASSET_INVENTORY = "KK";

        public static readonly string LIQUIDATION_CODE = "BTL";

        public static readonly string FINANCIAL_CODE = "CTTS";
        #endregion
        /// <summary>
        /// Số ký tự tối thiểu cho phần đuôi của AssetId
        /// (yyDD{xxxx} e.g. 23090123)
        /// </summary>
        public static readonly int AssetIdCodeMinDigit = 4;
        /// <summary>
        /// Số ký tự tối thiểu cho phần đuôi của FinancialCode
        /// </summary>
        public static readonly int AssetFinancialCodeMinDigit = 6;
        /// <summary>
        /// Tài sản đã cấp phát ( đang dùng )
        /// </summary>
        public const int ASSET_ALLOCATED = 1;

        /// <summary>
        /// Tài sản lỗi hỏng
        /// </summary>
        public const int ASSET_BROKEN = 2;
        /// <summary>
        /// Tài sản chưa cấp phát
        /// </summary>

        public const int ASSET_UNALLOCATED = 3;
        /// <summary>
        /// Tài sản đang sửa, bảo hành
        /// </summary>
        public const int ASSET_GUARANTEE = 4;
        /// <summary>
        /// Hủy
        /// </summary>
        public const int ASSET_CANCEL = 5;
        /// <summary>
        /// Mất
        /// </summary>
        public const int ASSET_LOST = 6;
        /// <summary>
        /// Thanh lý
        /// </summary>
        public const int ASSET_LIQUIDATION = 7;
        /// <summary>
        /// Id kho chính của Tài sản
        /// </summary>
        public static readonly int ASSET_MAIN_STOCK_ID = 1170;
        /// <summary>
        /// Mã code của kho chính của Tài sản
        /// </summary>
        public static readonly string ASSET_MAIN_STOCK_CODE = "1382";
        public static readonly int EXPORT_BASE_INDENT = 8;
        #region SheetName_export
        public static string S001_ASSET_LIST = "Danh sách tài sản";
        public static string S002_ASSET_DEPRECIATED = "Danh sách khấu hao";
        public static string S003_ASSET_ALLOCATED = "Danh sách cấp phát";
        public static string S004_ASSET_RECALLED = "Danh sách thu hồi";
        public static string S005_Asset_HANDOVER_DETAIL = "Danh sách bàn giao chi tiết";
        public static string S006_Asset_TYPE_LIST = "Danh mục loại tài sản";
        public static string S007_Asset_GROUP_LIST = "Danh mục nhóm tài sản";
        #endregion
        public static int MAX_PARENT_LEVEL = 5;
    }
}