using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Databases
{
	/// <summary>
	/// Mã các câu thông báo trong hệ thống.
	/// Author: System - Render tự động.
	/// Created at: 05/04/2022.
	/// </summary>
	public class MSG_NO
	{
		/// <summary>
		/// Bạn có thực sự muốn lưu dữ liệu này không?
		/// </summary>
		public static string CONFIRM_SAVE_DATA = "C001";
		/// <summary>
		/// Bạn có thực sự muốn xóa dữ liệu này không?
		/// </summary>
		public static string CONFIRM_DELETE_DATA = "C002";
		/// <summary>
		/// Đã lưu dữ liệu thành công, bạn có muốn trở về trang danh sách không?
		/// </summary>
		public static string CONFIRM_AFTER_SAVE = "C003";
		/// <summary>
		/// Đã xóa dữ liệu thành công, bạn có muốn trở về trang danh sách không?
		/// </summary>
		public static string CONFIRM_AFTER_DELETE = "C004";
		/// <summary>
		/// Bạn có thật sự muốn reset mật khẩu cho tài khoản này không?
		/// </summary>
		public static string CONFIRM_RESET_PASSWORD = "C005";
		/// <summary>
		/// Bạn có chắc chắn muốn thay đổi mật khẩu không?
		/// </summary>
		public static string CONFIRM_CHANGE_PASSWORD = "C006";
		/// <summary>
		/// Việc nhắc nhở sẽ được thực hiện tự động bởi hệ thống. Bạn có chắc chắn muốn tiếp tục nhắc nhở thủ công không?
		/// </summary>
		public static string CONFIRM_REMIND_ORDER_TRANSFER = "C007";
		/// <summary>
		/// Bạn có chắc chắn muốn xác nhận đã thanh toán cho đơn hàng này không?
		/// </summary>
		public static string CONFIRM_D0_CONFIRM_TRANSFER = "C008";
		/// <summary>
		/// Bạn có chắc chắn muốn mở khóa tài khoản này không?
		/// </summary>
		public static string CONFIRM_UNLOCK_ACCOUNT = "C009";
		/// <summary>
		/// Bạn có chắc chắn muốn gửi thông báo cho tất cả khách hàng?
		/// </summary>
		public static string CONFIRM_SEND_NOTIFICATION = "C010";
		/// <summary>
		/// Bạn có chắc chắn muốn gửi thông tin đã nhập đến khách hàng không?
		/// </summary>
		public static string CONFIRM_SEND_POINT_OR_VOUCHER_TO_CUSTOMER = "C011";
		/// <summary>
		/// Bạn đã thực hiện hoạt động này 1 lần rồi, bạn có muốn thực hiện lại 1 lần nữa không?
		/// </summary>
		public static string CONFIRM_SEND_POINT_OR_VOUCHER_TO_CUSTOMER_AGAIN = "C012";
		/// <summary>
		/// Đã gửi đến tất cả khách hàng thành công, bạn có muốn download kết quả gửi không?
		/// </summary>
		public static string CONFIRM_DOWN_RESULT_POINT_OR_VOUCHER = "C013";
		/// <summary>
		/// Bạn có chắc chắn muốn xác thực cho tài khoản này không?
		/// </summary>
		public static string CONFIRM_VERIFY_ACCOUNT = "C014";
		/// <summary>
		/// Bạn có chắc chắn muốn khoá tài khoản này không?
		/// </summary>
		public static string CONFIRM_LOCK_ACCOUNT = "C015";
		/// <summary>
		/// Bạn có chắc chắn muốn đưa tài khoản này vào blacklist hay không?
		/// </summary>
		public static string CONFIRM_ADD_ACCOUNT_TO_BLACK_LIST = "C016";
		/// <summary>
		/// Bạn có chắc chắn muốn xoá tài khoản này khỏi blacklist hay không?
		/// </summary>
		public static string CONFIRM_REMOVE_ACCOUNT_FROM_BLACK_LIST = "C017";
		/// <summary>
		/// Bạn có chắc chắn muốn gửi thông báo tin tức khuyến mãi này đến khách hàng đã chọn không?
		/// </summary>
		public static string CONFIRM_SEND_NOTIFY_PROMOTION_TO_CUSTOMER = "C018";
		/// <summary>
		/// Bạn đã thực hiện hoạt động này 1 lần rồi, bạn có muốn thực hiện lại 1 lần nữa không?
		/// </summary>
		public static string CONFIRM_SEND_NOTIFY_PROMOTION_TO_CUSTOMER_AGAIN = "C019";
		/// <summary>
		/// Bạn có thực sự muốn reset số lần đã gọi API về 0 hay không? Khi reset về 0, batch sẽ tự động gọi lại API này.
		/// </summary>
		public static string CONFIRM_RESET_VTT_API = "C020";
		/// <summary>
		/// Bạn có thực sự muốn reset việc gửi mail này không? Khi reset, batch sẽ tự động gửi lại email này.
		/// </summary>
		public static string CONFIRM_RESET_LOG_SENT_EMAIL = "C021";
		/// <summary>
		/// Bạn có thực sự muốn reset việc gửi thông báo này không? Khi reset, batch sẽ tự động gửi lại thông báo này.
		/// </summary>
		public static string CONFIRM_RESET_LOG_SENT_NOTIFY = "C022";
		/// <summary>
		/// Bạn có muốn tiếp nhận hỗ trợ này không?
		/// </summary>
		public static string CONFIRM_JOIN_CHAT = "C023";
		/// <summary>
		/// Bạn có muốn thoát khỏi hội thoại này không?
		/// </summary>
		public static string CONFIRM_LEAVE_CHAT = "C024";
		/// <summary>
		/// Bạn có  muốn kết thúc lần hỗ trợ này không?
		/// </summary>
		public static string CONFIRM_FINISH_CHAT = "C025";
		/// <summary>
		/// Bạn có thực sự muốn kết thúc phản hồi/góp ý/khiếu nại này không?
		/// </summary>
		public static string CONFIRM_FINISH_CLAIM = "C026";
		/// <summary>
		/// Bạn có thực sự muốn thay đổi loại của cuộc hội thoại này không?
		/// </summary>
		public static string CONFIRM_CHANGE_TYPE_CHAT = "C027";
		/// <summary>
		/// Sau khi xác nhận, đơn hàng sẽ không thể thay đổi dược nữa, bạn có muốntiếp tục không?
		/// </summary>
		public static string CONFIRM_CONFIRM_ORDER = "C028";
		/// <summary>
		/// Sau khi xác nhận thanh toán, đơn hàng sẽ tự động được xác nhận và không thể thay đổi dược nữa, bạn có muốn tiếp tục không?
		/// </summary>
		public static string CONFIRM_PAID_FOR_ORDER = "C029";
		/// <summary>
		/// Bạn có chắc chắn muốn huỷ đơn hàng này không?
		/// </summary>
		public static string CONFIRM_CANCEL_ORDER = "C030";
		/// <summary>
		/// Sau khi xác nhận đơn hàng đã hoàn thành, hệ thống sẽ tiến hành tích luỹ điểm cho khách hàng, bạn có muốn tiếp tục không?
		/// </summary>
		public static string CONFIRM_FINISH_ORDER = "C031";
		/// <summary>
		/// Bạn có chắc chắn muốn thay đổi trạng thái của đơn hàng này không?
		/// </summary>
		public static string CONFIRM_CHANGE_STATUS_ORDER = "C032";
		/// <summary>
		/// Bạn có chắc chắn muốn gửi email tin tức khuyến mãi này đến những người đã theo dõi không?
		/// </summary>
		public static string CONFIRM_SEND_EMAIL_PROMOTION_TO_SUBSCRIBER = "C033";
		/// <summary>
		/// Nội dung này không được để trống.
		/// </summary>
		public static string REQUIRED = "E001";
		/// <summary>
		/// Email không đúng định dạng.
		/// </summary>
		public static string EMAIL_FORMAT_RON = "E002";
		/// <summary>
		/// Ngày tháng không đúng định dạng.
		/// </summary>
		public static string DATE_FORMAT_RON = "E003";
		/// <summary>
		/// Đường dẫn sai định dạng.
		/// </summary>
		public static string URL_FORMAT_RON = "E004";
		/// <summary>
		/// Bạn không được phép nhập quá {0} kí tự.
		/// </summary>
		public static string EXCEED_MAXLENGTH = "E005";
		/// <summary>
		/// Bạn phải nhập tối thiểu {1} kí tự.
		/// </summary>
		public static string NOT_ENOUGH_MIN_LENGTH = "E006";
		/// <summary>
		/// Giá trị này phải lớn hơn hoặc bằng {2}.
		/// </summary>
		public static string VALUE_MUST_GEATER = "E007";
		/// <summary>
		/// Giá trị này phải nhỏ hơn hoặc bằng {3}.
		/// </summary>
		public static string VALUE_MUST_LESSER = "E008";
		/// <summary>
		/// Số điện thoại sai định dạng.
		/// </summary>
		public static string PHONE_NUMBER_FORMAT_RON = "E009";
		/// <summary>
		/// Thời gian bắt đầu phải bé hơn thời gian kết thúc.
		/// </summary>
		public static string TIME_FROM_MUST_LESSER_TIME_TO = "E010";
		/// <summary>
		/// Giá trị này đang bị trùng lặp.
		/// </summary>
		public static string DATA_DUPLICATE = "E011";
		/// <summary>
		/// Tài khoản không tồn tại.
		/// </summary>
		public static string USERNAME_NOT_INCORRECT = "E012";
		/// <summary>
		/// Mật khẩu không chính xác.
		/// </summary>
		public static string PASSWORD_NOT_INCORRECT = "E013";
		/// <summary>
		/// Số điện thoại hoặc mật khẩu không chính xác, vui lòng nhập lại.
		/// </summary>
		public static string USERNAME_OR_PASSWORD_NOT_INCORRECT = "E014";
		/// <summary>
		/// Tên đăng nhập này đã được đăng ký. Vui lòng sử dụng Tên đăng nhập khác!
		/// </summary>
		public static string USERNAME_HAD_USED = "E015";
		/// <summary>
		/// Hệ thống phải tồn tại ít nhất 1 tài khoản để sử dụng.
		/// </summary>
		public static string SYSTEM_MUST_HAVE_LEAST_1_USER = "E016";
		/// <summary>
		/// Mật khẩu phải chứa ít nhất 8 ký tự, có chứa chữ hoa, chữ thường và ký tự đặc biệt.
		/// </summary>
		public static string PASSWORD_WRONG_FORMAT = "E017";
		/// <summary>
		/// Mật khẩu xác nhận không trùng khớp, vui lòng kiểm tra lại.
		/// </summary>
		public static string CORNFIRM_PASSWORD_ERROR = "E018";
		/// <summary>
		/// Tên đăng nhập phải có ít nhất 4 ký tự, chỉ được nhập các ký tự chữ cái, chữ số và các dấu chấm (.), gạch nối (-), gạch dưới (_).
		/// </summary>
		public static string USERNAME_WRONG_FORMAT = "E019";
		/// <summary>
		/// File vượt quá dung lượng cho phép.
		/// </summary>
		public static string FILE_SIZE_TOO_LARGER = "E020";
		/// <summary>
		/// File không đúng định dạng.
		/// </summary>
		public static string EXTENSION_NOT_ALLOW = "E021";
		/// <summary>
		/// Không thể upload quá {0} file cùng lúc.
		/// </summary>
		public static string CANNOT_UPLOAD_MANY_FILES = "E022";
		/// <summary>
		/// Có lỗi trong quá trình tải file, vui lòng thử lại.
		/// </summary>
		public static string UPLOAD_FILE_ERROR = "E023";
		/// <summary>
		/// Vui lòng chọn file tải lên.
		/// </summary>
		public static string YOU_MUST_CHOOSE_FILE = "E024";
		/// <summary>
		/// Kích thước hình ảnh không đúng yêu cầu.
		/// </summary>
		public static string SIZE_OF_IMG_RON = "E025";
		/// <summary>
		/// Không thể thực hiện thao tác này, vì số item hiển thị trên trang chủ đã đạt mức tối thiểu.
		/// </summary>
		public static string EROR_MIN_ITEM_SHOW = "E026";
		/// <summary>
		/// Link này chỉ có thể chứa chuỗi không dấu bao gồm chữ thường, chữ hoa & dấu gạch nối (-).
		/// </summary>
		public static string BEAUTYID_WRONG_FORMAT = "E027";
		/// <summary>
		/// Lưu dữ liệu không thành công, vui lòng thử lại.
		/// </summary>
		public static string SAVE_DATA_ERROR = "E028";
		/// <summary>
		/// Xóa dữ liệu không thành công, vui lòng thử lại.
		/// </summary>
		public static string DELETE_DATA_ERROR = "E029";
		/// <summary>
		/// Không thể hiển thị số lượng kết quả quá lớn.
		/// </summary>
		public static string NUMBER_OF_RECORD_LARGER = "E030";
		/// <summary>
		/// Có lỗi trong quá trình tạo file, vui lòng thử lại.
		/// </summary>
		public static string CREATE_JS_ERROR = "E031";
		/// <summary>
		/// Dữ liệu này đã tồn tại trong hệ thống
		/// </summary>
		public static string DATA_EXISTING = "E032";
		/// <summary>
		/// Mã của thông báo đã tồn tại trong hệ thống
		/// </summary>
		public static string MESSAGE_CODE_EXISTING = "E033";
		/// <summary>
		/// Mã của chức năng đã tồn tại trong hệ thống
		/// </summary>
		public static string FUNCTION_CODE_EXISTING = "E034";
		/// <summary>
		/// Đang có tài khoản thuộc nhóm này, nên không thể xóa, hãy xóa hết tài khoản của nhóm này trước
		/// </summary>
		public static string GROUP_HAVE_ACCOUNT = "E035";
		/// <summary>
		/// Không có dữ liệu export
		/// </summary>
		public static string DATA_EXPORT_NOT_EXISTS = "E036";
		/// <summary>
		/// Chức năng này hiện không hoạt động trên mobile
		/// </summary>
		public static string FUNCTION_NOT_VALID_IN_MOBILE = "E037";
		/// <summary>
		/// Email này không tồn tại
		/// </summary>
		public static string EMAIL_NOT_INCORRECT = "E038";
		/// <summary>
		/// Quá hạn để thay đổi mật khẩu
		/// </summary>
		public static string TOKEN_TIMEOUT = "E039";
		/// <summary>
		/// Email này đã được đăng ký. Vui lòng sử dụng email khác!
		/// </summary>
		public static string EMAIL_HAD_USED = "E040";
		/// <summary>
		/// Mã của nhóm tài khoản đã tồn tại trong hệ thống
		/// </summary>
		public static string GROUP_ID_EXISTING = "E041";
		/// <summary>
		/// Số tiền chuyển khoản không chính xác
		/// </summary>
		public static string AMOUNT_NOT_CORECT = "E042";
		/// <summary>
		/// Mã OTP không chính xác hoặc hết hạn
		/// </summary>
		public static string OTP_NOT_INCORRECT_OR_TIMEOUT = "E043";
		/// <summary>
		/// Email hoặc số điện thoại không đúng
		/// </summary>
		public static string EMAIL_OR_PHONE_NOT_INCORRECT = "E044";
		/// <summary>
		/// Bạn đã yêu cầu OTP quá nhiều lần, vui lòng thử lại sau
		/// </summary>
		public static string REQUEST_OTP_TOO_MANY = "E045";
		/// <summary>
		/// Số điện thoại này đã được đăng ký. Vui lòng sử dụng số điện thoại khác!
		/// </summary>
		public static string PHONE_ALREADY_EXISTS = "E046";
		/// <summary>
		/// Chúng tôi đã nhận được đánh giá của tài khoản này trước đó rồi
		/// </summary>
		public static string REVIEW_EXISTING = "E047";
		/// <summary>
		/// Hoá đơn không tồn tại
		/// </summary>
		public static string ORDER_NOT_EXISTS = "E048";
		/// <summary>
		/// Câu hỏi bảo mật đang được sử dụng
		/// </summary>
		public static string SECURITY_QUESTION_HAD_USED = "E049";
		/// <summary>
		/// Yêu cầu nhập Captcha
		/// </summary>
		public static string REQUIRED_CAPTCHA = "E050";
		/// <summary>
		/// Vượt quá số ký tự cho phép
		/// </summary>
		public static string CONTENT_LENGTH_LIMIT = "E051";
		/// <summary>
		/// Captcha không hợp lệ
		/// </summary>
		public static string CAPTCHA_INVALID = "E052";
		/// <summary>
		/// Vượt quá số lần có thể đăng ký tài khoản trong 1 ngày
		/// </summary>
		public static string REGISTER_LIMIT_OVER = "E053";
		/// <summary>
		/// Tài khoản đã bị tạm khoá, vui lòng liên hệ ngay với bộ phận Chăm Sóc Khách Hàng của chúng tôi theo số điện thoại 090.1800.888  (Bấm số 8) để được hỗ trợ.
		/// </summary>
		public static string ACCOUNT_WAS_LOCKED = "E054";
		/// <summary>
		/// Vượt quá số lần cho phép chỉnh sửa trong ngày
		/// </summary>
		public static string CHANGE_INFO_LIMIT_OVER = "E055";
		/// <summary>
		/// Vui lòng nhập nhận xét đánh giá khi từ chối xác minh tài khoản.
		/// </summary>
		public static string COMMENT_WHEN_REJECT_VERIFICATION = "E056";
		/// <summary>
		/// Không thể gửi thông báo lúc này do trạng thái chương trình khuyến mãi đang "Ẩn", vui lòng chuyển sang trạng thái "Hiển thị" và thử lại.
		/// </summary>
		public static string CAN_NOT_SEND_NOTIFICATION_FOR_HIDDEN_PROMOTION = "E057";
		/// <summary>
		/// Mã quét được đã hết hạn hoặc không chính xác
		/// </summary>
		public static string QR_CODE_INVALID = "E058";
		/// <summary>
		/// Không thể tặng nhiều hơn số điểm bạn sở hữu. Vui lòng kiểm tra lại!
		/// </summary>
		public static string POINT_LIMIT = "E059";
		/// <summary>
		/// Số điện thoại nhận không đúng hoặc không sử dụng FM Plus!
		/// </summary>
		public static string PHONE_INCORRECT = "E060";
		/// <summary>
		/// Mã số mua hàng đã được sử dụng hoặc không thuộc sở hữu của bạn
		/// </summary>
		public static string VOUCHER_INVALID = "E061";
		/// <summary>
		/// Không có khách hàng nào được gửi
		/// </summary>
		public static string NO_CUSTOMER_SELECTED = "E062";
		/// <summary>
		/// Giá trị bắt đầu phải bé hơn giá trị kết thúc.
		/// </summary>
		public static string VALUE_FROM_MUST_LESSER_VALUE_TO = "E063";
		/// <summary>
		/// Tài khoản của bạn đã bị đưa vào blacklist, vui lòng liên hệ ngay với bộ phận Chăm Sóc Khách Hàng của chúng tôi theo số điện thoại 090.1800.888  (Bấm số 8) để được hỗ trợ
		/// </summary>
		public static string ACCOUNT_IN_BLACKLIST = "E064";
		/// <summary>
		/// Giá trị này không liên tiếp nhau
		/// </summary>
		public static string VALUE_IS_NOT_CONSECUTIVE = "E065";
		/// <summary>
		/// Phản ánh, khiếu nại của bạn đang được nhân viên chắm sóc khách hàng xử lý, nên không thể chỉnh sửa được
		/// </summary>
		public static string CAN_NOT_EDIT_CLAIM_IS_PROCESSING = "E066";
		/// <summary>
		/// Phản ánh,khiếu nại của bạn đang được nhân viên chắm sóc khách hàng xử lý, nên không thể  xoá được
		/// </summary>
		public static string CAN_NOT_DELETE_CLAIM_IS_PROCESSING = "E067";
		/// <summary>
		/// Góp ý của bạn đang được nhân viên chắm sóc khách hàng xử lý, nên không thể chỉnh sửa được
		/// </summary>
		public static string CAN_NOT_EDIT_FEEDBACK_IS_PROCESSING = "E068";
		/// <summary>
		/// Góp ý của bạn đang được nhân viên chắm sóc khách hàng xử lý, nên không thể xoá được
		/// </summary>
		public static string CAN_NOT_DELETE_FEEDBACK_IS_PROCESSING = "E069";
		/// <summary>
		/// Không tìm thấy yêu cầu chăm sóc khách hàng
		/// </summary>
		public static string NOT_FOUND_CONVENTION = "E070";
		/// <summary>
		/// Yêu cầu chắm sóc khách hàng đang được xử lý bởi nhân viên chăm sóc kách hàng khác
		/// </summary>
		public static string COMVENTION_IS_PROCESSING = "E071";
		/// <summary>
		/// Yêu cầu chắm sóc khách hàng đã hoàn thành
		/// </summary>
		public static string COMVENTION_HAD_FINISHED = "E072";
		/// <summary>
		/// Không tồn tại đánh giá cho đơn hàng này
		/// </summary>
		public static string ORDER_REVIEW_EXISTS = "E073";
		/// <summary>
		/// Không tồn tại góp ý, phản ảnh, khiếu nại
		/// </summary>
		public static string CLAIM_NOT_EXISTS = "E074";
		/// <summary>
		/// Không tồn tại đánh giá cho góp ý, phản ảnh, khiếu nại này
		/// </summary>
		public static string CLAIM_REVIEW_EXISTS = "E075";
		/// <summary>
		/// Mã nhân viên đã tồn tại trong hệ thống
		/// </summary>
		public static string EMPLOYEE_ID_EXISTING = "E076";
		/// <summary>
		/// Đã vượt quá số lần chỉnh sửa tên đăng nhập trong ngày
		/// </summary>
		public static string CHANGE_USERNAME_LIMIT = "E077";
		/// <summary>
		/// Đã vượt quá số lần chỉnh sửa giới tính trong ngày
		/// </summary>
		public static string CHANGE_GENDER_LIMIT = "E078";
		/// <summary>
		/// Đã vượt quá số lần chỉnh sửa ngày sinh trong ngày
		/// </summary>
		public static string CHANGE_BIRTHDAY_LIMIT = "E079";
		/// <summary>
		/// Đã vượt quá số lần chỉnh sửa địa chỉ trong ngày
		/// </summary>
		public static string CHANGE_ADDRESS_LIMIT = "E080";
		/// <summary>
		/// Không thể xoá nội dung đã được đăng
		/// </summary>
		public static string CAN_NOT_DELETE_CONTENT_POSTED = "E081";
		/// <summary>
		/// Không thể chỉnh sửa nội dung đã được đăng
		/// </summary>
		public static string CAN_NOT_EDIT_CONTENT_POSTED = "E082";
		/// <summary>
		/// Đã vượt quá số lần đăng cho phép
		/// </summary>
		public static string LINIT_POST_ERROR = "E083";
		/// <summary>
		/// Thời gian đã chọn phải lớn hơn thời gian hiện tại
		/// </summary>
		public static string TIME_MUST_BE_LARGER_THAN_NOW = "E084";
		/// <summary>
		/// Tài khoản đã bị tạm khoá, vui lòng liên hệ quản trị hệ thống để mở lại tài khoản.
		/// </summary>
		public static string ADMIN_ACCOUNT_WAS_LOCKED = "E085";
		/// <summary>
		/// Dữ liệu không tồn tại
		/// </summary>
		public static string DATA_NOT_EXISTING = "E086";
		/// <summary>
		/// Đơn hàng không được phép hủy
		/// </summary>
		public static string ORDER_CANCEL_ERROR = "E087";
		/// <summary>
		/// Giá của sản phẩm đã thay đổi, vui lòng cập nhật lại giỏ hàng
		/// </summary>
		public static string SALE_PRICE_HAS_BEEN_CHANGED = "E088";
		/// <summary>
		/// Đã có lỗi xảy ra trong quá trình mua hàng
		/// </summary>
		public static string ORDER_ERROR = "E089";
		/// <summary>
		/// Mã SKU đã tồn tại trong hệ thống, vui lòng kiểm tra lại.
		/// </summary>
		public static string SKU_EXISTING = "E090";
		/// <summary>
		/// Bạn phải nhập thông tin số lượng sản phẩm
		/// </summary>
		public static string PRODUCT_QUANTITY_IS_REQUIRED = "E091";
		/// <summary>
		/// Không đủ điểm để thực hiện đơn hàng này, vui lòng kiểm tra lại
		/// </summary>
		public static string NOT_ENOUGHT_POINTS = "E092";
		/// <summary>
		/// Mã giảm giá đang sử dụng trong đơn hàng này đã được sử dụng hoặc đã hết hạn, vui lòng kiểm tra lại
		/// </summary>
		public static string VOUCHER_IN_ORDER_INVALID = "E093";
		/// <summary>
		/// Không tìm thấy đơn hàng đang xử lý, có thể đơn hàng đã bị xoá, vui lòng kiểm tra lại
		/// </summary>
		public static string ORDER_NOT_FOUND = "E094";
		/// <summary>
		/// Không thể thay đổi trạng thái của đơn hàng này
		/// </summary>
		public static string CAN_NOT_CHANGE_STATUS_OF_ORDER = "E095";
		/// <summary>
		/// Mã miễn phí vận chuyển đang sử dụng trong đơn hàng này đã được sử dụng hoặc đã hết hạn, vui lòng kiểm tra lại
		/// </summary>
		public static string FREE_SHIP_VOUCHER_INVALID = "E096";
		/// <summary>
		/// Trong đơn hàng có sản phẩm không còn đủ trong kho, vui lòng kiểm tra lại
		/// </summary>
		public static string NOT_ENOUGHT_PRODUCTS = "E097";
		/// <summary>
		/// Giá khuyến mãi phải nhỏ hơn giá hiện tại
		/// </summary>
		public static string SALE_PRICE_MUST_LOWER_THAN_CURRENT_PRICE = "E098";
		/// <summary>
		/// Số lượng sản phẩm khuyến mãi phải nhỏ hơn số lượng sản phẩm hiện tại
		/// </summary>
		public static string SALE_QUANTITY_MUST_LOWER_THAN_CURRENT_QUANTITY = "E099";
		/// <summary>
		/// Khung giờ bạn chọn đã có chương trình khuyến mãi khác diễn ra
		/// </summary>
		public static string TIME_OF_GOLD_HOUR_INCORRECT = "E100";
		/// <summary>
		/// Đã có giao diện khác hiển thị trong khoảng thời gian đã chọn
		/// </summary>
		public static string GUI_EXISTING = "E101";
		/// <summary>
		/// Sản phẩm đã tồn tại trong giờ vàng đang/sắp diễn ra
		/// </summary>
		public static string PRODUCT_EXISTING_IN_GOLDEN_HOUR = "E102";
		/// <summary>
		/// Sản phẩm đã tồn tại trong siêu sale đang/sắp diễn ra
		/// </summary>
		public static string PRODUCT_EXISTING_IN_FLASH_SALE = "E103";
		/// <summary>
		/// Không có sản phẩm phù hợp
		/// </summary>
		public static string PRODUCT_NOT_MATCH = "E104";
		/// <summary>
		/// Không tìm thấy sản phẩm
		/// </summary>
		public static string PRODUCT_NOT_FOUND = "E105";
		/// <summary>
		/// Đã đủ tối đa X hoá đơn. Vui lòng xoá bớt hoá đơn không dùng hoặc lưu tạm để tiếp tục.
		/// </summary>
		public static string INVOICE_IS_ENOUGH = "E106";
		/// <summary>
		/// Voucher không tồn tại
		/// </summary>
		public static string VOUCHER_NOT_EXISTS = "E107";
		/// <summary>
		/// Voucher không áp dụng cho khách hàng này
		/// </summary>
		public static string VOUCHER_NOT_APPLY_CURRENT_CUSTOMER = "E108";
		/// <summary>
		/// Voucher không áp dụng tại cửa hàng này
		/// </summary>
		public static string VOUCHER_NOT_APPLY_CURRENT_BRANCH = "E109";
		/// <summary>
		/// Nhân viên thu ngân không tồn tại
		/// </summary>
		public static string CASHIER_NOT_EXISTS = "E110";
		/// <summary>
		/// Nhân viên tư vấn không tồn tại
		/// </summary>
		public static string CONSULTANTS_NOT_EXISTS = "E111";
		/// <summary>
		/// Khách hàng không tồn tại
		/// </summary>
		public static string CUSTOMER_NOT_EXISTS = "E112";
		/// <summary>
		/// Điểm vàng hoặc Điểm bạc không sử dụng chung với Mã giảm giá
		/// </summary>
		public static string GOLD_SILVER_POINT_NOT_USE_WITH_VOUCHER = "E113";
		/// <summary>
		/// Điểm vàng không đủ
		/// </summary>
		public static string GOLD_POINT_NOT_ENOUGH = "E114";
		/// <summary>
		/// Điểm bạc không đủ
		/// </summary>
		public static string SILVER_POINT_NOT_ENOUGH = "E115";
		/// <summary>
		/// Bạn chưa nhập tiền Khách thanh toán
		/// </summary>
		public static string JUST_NOT_ENTER_MONEY_PAYMENT = "E116";
		/// <summary>
		/// Số tiền khách thanh toán không được âm
		/// </summary>
		public static string MONEY_PAYMENT_NOT_NEGATIVE = "E117";
		/// <summary>
		/// Không có sản phẩm nào cần thanh toán
		/// </summary>
		public static string NOT_PRODUCT_IS_NEED_PAYMENT = "E118";
		/// <summary>
		/// Sản phẩm {x, y, z, …} không tồn tại
		/// </summary>
		public static string PRODUCT_NOT_EXITST_IN_CART = "E119";
		/// <summary>
		/// Sản phẩm {x, y, z, …} không đủ tồn kho
		/// </summary>
		public static string PRODUCT_NOT_ENOUGH_IN_STOCK = "E120";
		/// <summary>
		/// Có lỗi trong quá trình tính toán, vui lòng thanh toán lại hoặc thanh toán từng sản phẩm
		/// </summary>
		public static string PAYMENT_PROCESS_IS_ERROR = "E121";
		/// <summary>
		/// Bạn không thể sử dụng lại mật khẩu cũ, vui lòng thử mật khẩu khác
		/// </summary>
		public static string NOT_USE_OLD_PASSWORD = "E122";
		/// <summary>
		/// Bạn phải chọn í nhất 1 dòng để xoá
		/// </summary>
		public static string SELECT_AT_LEAST_1_LINE = "E123";
		/// Bạn phải nhập số lượng nhiều hơn 20
		/// </summary>
		public static string QUANTITY_MUST_BE_MORE_THAN_20 = "E124";
		/// <summary>
		/// Bạn phải nhập số tiền nhiều hơn 1000
		/// </summary>
		public static string MONEY_MUST_BE_MORE_THAN_1000 = "E125";
		/// <summary>
		/// Voucher chưa đến hạn sử dụng
		/// </summary>
		public static string VOUCHER_NOT_USE_YET = "E126";
		/// <summary>
		/// Voucher đã hết hạn
		/// </summary>
		public static string VOUCHER_EXPIRED = "E127";
		/// <summary>
		/// Voucher đã hết số lần sử dụng
		/// </summary>
		public static string VOUCHER_OVER_COUNT_USER = "E128";
		/// <summary>
		/// Không thể sử dụng voucher vì chưa đạt giá trị đơn hàng tối thiểu
		/// </summary>
		public static string VOUCHER_ORDER_VALUE_MINIMUM = "E129";
		/// <summary>
		/// Số tiền giảm giá của Mã giảm giá vượt quá giá trị tối đa, vui lòng thử lại
		/// </summary>
		public static string VOUCHER_OVER_MAX_DISCOUNT = "E130";
		/// <summary>
		/// {0} không tồn tại
		/// </summary>
		public static string SOMETHING_NOT_EXISTS = "E131";
		/// <summary>
		/// Không được nhập ngày quá khứ
		/// </summary>
		public static string NOT_USE_PAST_DAY = "E132";
		/// <summary>
		/// Ngày nhập kho không được nhỏ hơn ngày xuất kho
		/// </summary>
		public static string EXPORT_DAY_NOT_LESSER_IMPORT_DAY = "E133";
		/// <summary>
		/// Không tìm thấy chi nhánh
		/// </summary>
		public static string BRANCH_NOT_FOUND = "E134";
		/// <summary>
		/// Nhân viên không thuộc chi nhánh này
		/// </summary>
		public static string EMPLOYEE_DOES_NOT_EXIST_AT_THIS_BRANCH = "E135";
		/// <summary>
		/// Người kiểm hàng không thuộc chi nhánh này
		/// </summary>
		public static string CHECKER_DOES_NOT_EXITS_AT_THIS_BRANCH = "E136";
		/// <summary>
		/// Người xuất kho không thuộc chi nhánh này
		/// </summary>
		public static string EXPORTER_DOES_NOT_EXITS_AT_THIS_BRANCH = "E137";
		/// <summary>
		/// Loại xuất kho không đúng
		/// </summary>
		public static string EXPORT_TYPE_INCORRECT = "E138";
		/// <summary>
		/// Không tìm thấy nhà cung cấp hàng phù hợp
		/// </summary>  
		public static string VENDOR_NOT_FOUND = "E139";
		/// <summary>
		/// Không tìm thấy sản phẩm trong kho
		/// </summary>
		public static string PRODUCT_NOT_FOUND_IN_STOCK = "E140";
		/// <summary>
		/// Danh sách sản phẩm trống
		/// </summary>
		public static string LIST_OF_PRODUCT_IS_EMPTY = "E141";
		/// <summary>
		/// Đơn xuất kho không tồn tại
		/// </summary>
		public static string EXPORT_ORDER_IS_NOT_EXISTS = "E142";
		/// <summary>
		/// Người nhập kho không thuộc chi nhánh này
		/// </summary>
		public static string IMPORTER_DOES_NOT_EXITS_AT_THIS_BRANCH = "E143";
		/// <summary>
		/// Loại nhập kho không đúng
		/// </summary>
		public static string IMPORT_TYPE_INCORRECT = "E144";
		/// <summary>
		/// Đơn nhập kho không tồn tại
		/// </summary>
		public static string IMPORT_ORDER_IS_NOT_EXISTS = "E145";
		/// <summary>
		/// Sản phẩm chưa tồn tại trong hệ thống
		/// </summary>
		public static string PRODUCT_IS_NOT_EXITS_IN_SYSTEM = "E146";
		/// <summary>
		/// Số lượng sản phẩm nhập kho phải nhỏ hơn số lượng sản phẩm xuất kho
		/// </summary>
		public static string IMPORT_QUANTITY_MUST_LESSER_EXPORTQUANTITY = "E147";
		/// <summary>
		/// Không thể thực hiện chuyển hàng trong cùng một chi nhánh
		/// </summary>
		public static string CANNOT_TRANSFER_STOCK_IN_THE_SAME_BRANCH = "E148";
		/// <summary>
		/// Đơn chuyển kho không tồn tại
		/// </summary>
		public static string TRANSFER_ORDER_IS_NOT_EXISTS = "E149";
		/// <summary>
		/// Cửa hàng đã tồn tại
		/// </summary>
		public static string BRANCH_HAD_USED = "E150";

		/// <summary>
		/// Sản phẩm đã tồn tại trong kho, không thể xoá sản phầm này
		/// </summary>
		public static string PRODUCT_EXISTING_IN_STOCK = "E151";

		/// <summary>
		/// Mã ngành hàng không được rỗng
		/// </summary>
		public static string CATEGORY_CODE_IS_EMPTY = "E152";

		/// <summary>
		/// Mã Nhóm/Ngành hàng không được để trống
		/// </summary>
		public static string CATEGORY_GENDER_CODE_IS_EMPTY = "E153";

		/// <summary>
		/// Tên nhóm hàng đã tồn tại
		/// </summary>
		public static string CATEGORY_NAME_IS_EXIST = "E154";

		/// <sumary>
		/// Số điểm tích luỹ tối đa khi sử dụng không được bé hơn sô điểm tích luỹ tối thiểu sử dụng
		/// </summary>
		public static string SETTING_POINT_IS_ERROR = "E155";

		/// <sumary>
		/// Đường dẫn đã tồn tại
		/// </summary>
		public static string SLUG_IS_EXIST = "E156";

		/// <summary>
		/// Ngày bắt đầu không được nhỏ hơn ngày kết thức
		/// </summary>
		public static string FROM_DAY_NOT_LESSER_TO_DAY = "E157";
		
		/// <summary>
		/// Thời gian bắt đầu không được nhỏ hơn thời gian kết thức
		/// </summary>
		public static string FROM_TIME_NOT_LESSER_TO_TIME = "E158";
		
		/// <summary>
		/// Họ tên đã tồn tại
		/// </summary>
		public static string FULLNAME_EXISTING = "E159";

		/// <summary>
		/// Sản phẩm con đã tồn tại
		/// </summary>
		public static string PRODUCT_COLOR_AND_SIZE_EXISTING = "E160";
		/// <summary>
		/// Đã hết ngày phép
		/// </summary>
		public static string LEAVES_IS_OVER = "E161";
		/// <summary>
		/// Đã có đơn
		/// </summary>
		public static string REQUISITON_ALREADY_EXIST = "E162";
        // <summary>
        /// Sản phẩm không phù hợp với mã SKU.
        /// </summary>
        public static string PRODUCT_NOT_MATCH_SKU_CHECK = "E163";
        /// <summary>
        /// Sản phẩm không phù hợp với mã ngành.
        /// </summary>
        public static string PRODUCT_NOT_MATCH_GENDER_CHECK = "E164";
        /// <summary>
        /// Sản phẩm không phù hợp với mã nhóm.
        /// </summary>
        public static string PRODUCT_NOT_MATCH_CATEGORY_CHECK = "E165";

		/// <summary>
        /// Nhóm quyền đã tồn tại tài khoản nên không thể xoá.
        /// </summary>
        public static string ROLE_ALREADY_HAVE_USERS = "E166";

        /// <summary>
        /// Dữ liệu truyền vào không đúng
        /// </summary>
        public static string INPUT_DATA_IS_INCORRECT = "E192";

		/// <summary>
		/// Số giờ vắng mặt không được lớn hơn 4
		/// </summary>
		public static string HOUR_OF_ABSENT_SHOULD_NOT_BE_THAN_4 = "E168";
		/// <summary>
		/// Mã khối đã tồn tại
		/// </summary>
		public static string EXIST_CODE_BLOCK = "E169";
		/// <summary>
		/// Tên khối đã tồn tại
		/// </summary>
		public static string EXIST_NAME_BLOCK = "E170";
		/// <summary>
		/// Không có đơn tăng ca vào ngày này
		/// </summary>
		public static string NO_REQUISITION_OVERTIME_ON_DAY = "E171";
		/// <summary>
		/// Số giờ bù lớn hơn số giờ làm
		/// </summary>
		public static string MAKEUP_MINUTES_GREATER_THAN_WORK_TIME = "E172";

        /// <summary>
        /// Tài khoản ngân hàng không tồn tại
        /// </summary>
        public static string BANK_ACCOUNT_DOES_NOT_EXISTS = "E173";

        /// <summary>
        /// Không tìm thấy ngày tạo
        /// </summary>
        public static string TRANSACTION_DATE_NOT_FOUND = "E174";

        /// Không tìm sổ quỹ
        /// </summary>
        public static string TRANSACTION_NOT_FOUND = "E175";

		/// <summary>
        /// Bạn chưa checkout ca trước
        /// </summary>
        public static string YOU_HAVE_NOT_CHECKOUT_THE_PREVIOUS_SHIFT = "E176";

		/// <summary>
        /// Tài khoản kế toán đã sử dụng, không thể xoá
        /// </summary>
        public static string ACCOUNT_IS_USED = "E177";

        /// <summary>
        /// Không thể sửa phiếu thu/chi
        /// </summary>
        public static string CAN_NOT_EDIT_RECEIPT_EXPENSE = "E179";

        /// <summary>
        /// Không thể xoá phiếu thu/chi
        /// </summary>
        public static string CAN_NOT_DELETE_RECEIPT_EXPENSE = "E180";

        /// <summary>
        /// Không thể thay đổi trạng thái của phiếu thu tự động
        /// </summary>
        public static string CAN_NOT_BE_CHANGED_STATUS_OF_AUTOMATIC_RECEIPT = "E181";

		/// <summary>
		/// Đã có đơn xin nghỉ 1 ngày
		/// </summary>
		public static string EXIST_REQUISITION_PAIDLEAVE_ONE_DAY = "E190";

		/// <summary>
		/// Đã có đơn xin nghỉ nhiều ngày
		/// </summary>
		public static string EXIST_REQUISITION_PAIDLEAVE_MANY_DAYS = "E182";

		/// <summary>
		/// Đã có đơn xin nghỉ buổi sáng
		/// </summary>
		public static string EXIST_REQUISITION_PAIDLEAVE_MORNING = "E183";

		/// <summary>
		/// Đã có đơn xin nghỉ buổi chiều
		/// </summary>
		public static string EXIST_REQUISITION_PAIDLEAVE_AFTERNOON = "E184";

		/// <summary>
		/// Đã có đơn checkn
		/// </summary>
		public static string EXIST_REQUISITION_CHECKIN = "E185";

		/// <summary>
		/// Đã có đơn vắng mặt
		/// </summary>
		public static string EXIST_REQUISITION_ABSENT = "E186";

		/// <summary>
		/// Đã có đơn tăng ca
		/// </summary>
		public static string EXIST_REQUISITION_OVERTIME = "E187";

		/// <summary>
		/// Đã có đơn bù giờ
		/// </summary>
		public static string EXIST_REQUISITION_MAKEUP = "E188";

		/// <summary>
		/// Đơn xin nghỉ chứa thời gian checkin
		/// </summary>
		public static string CONTAIN_CHECKIN = "E189";

        /// <summary>
        /// Từ dòng phải nhỏ hơn đến dòng
        /// </summary>
        public static string FROM_ROW_MUST_BE_SMALLER_TO_ROW = "E191";

		/// <summary>
		/// Thời gian nghỉ phải thuộc thời gian làm
		/// </summary>
		public static string BREAK_TIME_MUST_BELONG_TO_WORKING_TIME = "E193";

		/// <summary>
		/// Tên kiểu nghỉ đã tồn tại
		/// </summary>
		public static string NAME_VACATION_EXIST = "E194";

		/// <summary>
		/// Danh sách phiếu thu/chi trống
		/// </summary>
		public static string LIST_RECEIPT_AND_EXPENSE_EMPTY = "E195";
		
		/// <summary>
		/// Tên ca đã tồn tại
		/// </summary>
		public static string NAME_SHIFT_EXIST = "E196";

		/// <summary>
		/// Dữ liệu đã được thay đổi, vui lòng kiểm tra lại
		/// </summary>
		public static string EXCLUSIVE_ERROR = "E197";

		/// <summary>
		/// Đã checkin checkout không thể hoàn đơn tăng ca
		/// </summary>
		public static string CAN_NOT_UNDO_REQUISITION_OVERTIME = "E198";
		
		/// <summary>
		/// Có đơn trong quá trình checkin checkout
		/// </summary>
		public static string THERE_IS_A_REQUISITION_DURING_CHECKIN_CHECKOUT = "E199";

		/// <summary>
		/// Danh sách phiếu công nợ NCC trống
		/// </summary>
		public static string LIST_VENDOR_RECEIVABLE_DEBT_EMPTY = "E200";

		/// <summary>
		/// Danh sách sao kê ngân hàng trống
		/// </summary>
		public static string LIST_BANK_STATEMENT_EMPTY = "E201";

		/// <summary>
		/// Thời gian checkin không được muộn hơn thời gian kết thúc tăng ca trong đơn
		/// </summary>
		public static string CHECKIN_OVERTIME_LATE = "E202";
		
		/// <summary>
		/// Vượt quá số đơn tối đa trong tháng
		/// </summary>
		public static string EXECEED_THE_MAXIMUM_NUMBER_OF_REQUISITION_PER_MONTH = "E203";
		
		/// <summary>
		/// Không thuộc khoảng thời gian có thể tạo đơn
		/// </summary>
		public static string NOT_WITHIN_THE_TIME_PERIOD_WHERE_THE_REQUISITION_CAN_BE_CREATED = "E204";
		
		/// <summary>
		/// Không thể tạo 2 ca cùng 1 ngày
		/// </summary>
		public static string CAN_NOT_CREATE_2_SHIFTS_ON_THE_SAME_DAY = "E205";
		
		/// <summary>
		/// Ngân hàng không tồn tại
		/// </summary>
		public static string BANK_DOES_NOT_EXISTS = "E206";
		/// <summary>
		/// Kế toán đã duyệt, không thể thay đổi trạng thái
		/// </summary>
		public static string APPROVED_BY_ACCOUNTANT_CANNOT_CHANGE_STATUS = "E207";

		/// <summary>
		/// Kế toán không duyệt, không thể thay đổi trạng thái
		/// </summary>
		public static string NOT_APPROVED_BY_ACCOUNTANT_CANNOT_CHANGE_STATUS = "E208";

		/// <summary>
		/// Kí hiệu ca đã tồn tại
		/// </summary>
		public static string CODE_SHIFT_EXIST = "E209";
		/// <summary>
		/// Danh sách sản phẩm chiết khấu trống.
		/// </summary>
		public static string LIST_OF_DISCOUNT_PRODUCT_IS_EMPTY = "E210";
		/// <summary>
		/// Thông tin chiết khấu của sản phẩm không đúng.
		/// </summary>
		public static string DISCOUNT_PRODUCT_INFO_IN_CORRECT = "E211";
		/// <summary>
		/// Tổng tiền chiết khấu cho đơn hàng không đúng.
		/// </summary>
		public static string TOTAL_DISCOUNT_ORDER_IN_CORRECT = "E212";
		/// <summary>
		/// Tổng tiền chiết khấu cho từng sản phẩm không đúng.
		/// </summary>
		public static string TOTAL_DISCOUNT_PRODUCT_IN_CORRECT = "E213";
		/// <summary>
		/// Danh sách sản phẩm tặng trống.
		/// </summary>
		public static string LIST_OF_GIFT_PRODUCT_IS_EMPTY = "E214";
		/// <summary>
		/// Số lượng sản phẩm tặng không đúng.
		/// </summary>
		public static string GIFT_PRODUCT_QUANTITY_IN_CORRECT = "E215";
		/// <summary>
		/// Sản phẩm tặng không nằm trong danh sách được tặng.
		/// </summary>
		public static string GIFT_PRODUCT_IS_NOT_FOUND_IN_LIST = "E216";
		/// <summary>
		/// Không tìm thấy sản phẩm tặng trong hệ thống.
		/// </summary>
		public static string GIFT_PRODUCT_IS_NOT_FOUND_IN_SYSTEM = "E217";
		/// <summary>
		/// Không tìm thấy sản phẩm tặng trong kho.
		/// </summary>
		public static string GIFT_PRODUCT_IS_NOT_FOUND_IN_STOCK = "E218";
		/// <summary>
		/// Số lượng sản phẩm tặng vượt quá số lượng tồn kho.
		/// </summary>
		public static string GIFT_PRODUCT_QUANTITY_MORE_THAN_QUANTITY_IN_STOCK = "E219";
		/// <summary>
		/// Không tìm thấy thông tin địa chỉ của khách hàng.
		/// </summary>
		public static string USER_ADDRESS_IS_NOT_FOUND = "E220";
		/// <summary>
		/// Không tìm thấy phương thức vận chuyển phù hợp cho đơn hàng.
		/// </summary>
		public static string SHIPMENT_METHOD_IS_NOT_VALID = "E221";
		/// <summary>
		/// Số tiền khách hàng thanh toán phải lớn hơn hoặc bằng tổng tiền của đơn hàng.
		/// </summary>
		public static string CUSTOMER_MONEY_MUST_BE_GREATER_THAN_TOTAL_PAYMENT = "E222";
		/// <summary>
		/// Tổng tiền thanh toán từ các hình thức thanh toán không bằng tổng tiền khách trả.
		/// </summary>
		public static string TOTAL_PAYMENT_FROM_PAYMENTS_METHOD_IS_NOT_EQUAL_CUSTOMER_MONEY = "E223";
		/// <summary>
		/// Số lượng trả lớn hơn số lượng mua (có thể đã trả trước đó), vui lòng kiểm tra lại hoá đơn.
		/// </summary>
		public static string RETURN_PRODUCT_QUANTITY_IN_CORRECT = "E224";
		/// <summary>
		/// Tiền trả lại của sản phẩm không đúng.
		/// </summary>
		public static string RETURN_MONEY_OF_PRODUCT_IN_CORRECT = "E225";
		/// <summary>
		/// Số điểm trả lại cho khách hàng không đúng.
		/// </summary>
		public static string RETURN_POINT_QUANTIY_IN_CORRECT = "E226";
		/// <summary>
		/// Không thể tạo phiếu vì quá hạn. Vui lòng liên hệ bộ phận kế toán.
		/// </summary>
		public static string CAN_NOT_CREATE_VOUCHER_BECAUSE_WAS_OVERDUE = "E227";
		/// <summary>
		/// Danh sách sản phẩm không đúng. Vui lòng kiểm tra lại danh sách!
		/// </summary>
		public static string LIST_OF_PRODUCT_IN_CORRECT = "E228";
		/// <summary>
		/// Đơn hàng đã bị huỷ, không thể thay đổi trạng thái
		/// </summary>
		public static string ORDER_CANCELED_CANNOT_CHANGE_STATUS = "E229";
        /// <summary>
        /// Không thể thay đổi phương thức thanh toán vì quá hạn. Vui lòng liên hệ bộ phận kế toán.
        /// </summary>
        public static string CHANGE_EXPIRED = "E231";
		/// <summary>
		/// Không tìm thấy loại chi nhánh phù hợp
		/// </summary>
		public static string TYPE_OF_BRANCH_NOT_FOUND = "E232";
		/// <summary>
		/// Không tìm thấy Tỉnh/Thành phố phù hợp
		/// </summary>
		public static string PROVINCE_NOT_FOUND = "E233";
		/// <summary>
		/// Không tìm thấy Quận/Huyện phù hợp
		/// </summary>
		public static string DISTRICT_NOT_FOUND = "E234";
		/// <summary>
		/// Không tìm thấy Xã/Phường phù hợp
		/// </summary>
		public static string COMMUNES_NOT_FOUND = "E235";
		/// <summary>
		/// Giá trị chiết khấu không phù hợp với thứ hạng hiện tại của khách hàng
		/// </summary>
		public static string DISCOUNT_VALUE_IS_NOT_MATCH_RANK_OF_CUSTOMER = "E236";
		/// <summary>
		/// Tổng tiền khách hàng thanh toán từ các hình thức khác tiền mặt phải nhỏ hơn tổng tiền thanh toán
		/// </summary>
		public static string PAYMENTS_METHOD_NO_CASE_MUST_LESSER_TOTAL_PAYMENT = "E237";
		/// <summary>
		/// Tháng nhập vào phải đúng định dạng MM/YYYY
		/// </summary>
		public static string INVALID_MONTH = "E238";
		/// <summary>
		/// Phòng ban chưa có miền
		/// </summary>
		public static string INVALID_DEPARTMENT = "E239";

		/// <summary>
		/// Mã đơn hàng không tồn tại trong chi nhánh đã chọn
		/// </summary>
		public static string ORDER_CODE_NOT_BELONG_TO_BRANCH = "E240";

		/// <summary>
		/// Danh sách phiếu công nợ nhân viên trống
		/// </summary>
		public static string LIST_EMPLOYEE_RECEIVABLE_DEBT_EMPTY = "E241";

		/// <summary>
		/// Danh mục không tồn tại hoặc không được sử dụng nữa
		/// </summary>
		public static string INVALID_CATEGORY = "E242";

		/// <summary>
		/// Nhà cung cấp không hợp lệ
		/// </summary>
		public static string INVALID_VENDOR = "E243";

		/// <summary>
		/// Kế toán công nợ đang sử dụng
		/// </summary>
		public static string NCC_INUSE = "E244";

		/// <summary>
		/// Tài nguyên đang sử dụng
		/// </summary>
		public static string RESOURCE_INUSE = "E245";

		/// <summary>
		/// Không thể chỉnh sửa kế hoạch công tác đã duyệt
		/// </summary>
		public static string WORKPLAN_APPROVED = "E246";

		/// <summary>
		/// Loại cài đặt người duyệt đã tồn tại
		/// </summary>
		public static string APPROVE_SETTING_EXIST = "E247";

		/// <summary>
		/// Không thể gửi thông báo vì mã của thiết bị của người duyệt trên OneSignal trống
		/// </summary>
		public static string EMPTY_PLAYERID = "E248";

		/// <summary>
		/// Không thể duyệt kế hoạch công tác
		/// </summary>
		public static string CAN_NOT_APPROVED = "E249";

		/// <summary>
		/// Kế hoạch công tác này đã tạm ứng chi phí rồi
		/// </summary>
		public static string EXIST_WORK_ADVANCE = "E250";

		/// <summary>
		/// Mã công nợ này đã tồn tại.
		/// </summary>
		public static string EXIST_ARID = "E251";

		/// <summary>
		/// Không thể xoá dự trù chi phí đã thanh toán.
		/// </summary>
		public static string CAN_NOT_DELETE_ADVANCE = "E252";

		/// <summary>
		/// Không thể xoá kế hoạch công tác.
		/// </summary>
		public static string CAN_NOT_DELETE_WORKPLAN = "E253";

		/// <summary>
		/// Kế hoạch công tác đã tạo báo cáo rồi.
		/// </summary>
		public static string REPORTED = "E254";

		/// <summary>
		/// Kế hoạch công tác chưa tạo báo cáo.
		/// </summary>
		public static string NOT_REPORTED = "E255";

		/// <summary>
		/// Không thể xoá báo cáo kế hoạch công tác.
		/// </summary>
		public static string CAN_NOT_DELETE_REPORT_WORKPLAN = "E256";

        /// <summary>
        /// Đã submit khảo sát
        /// </summary>
        public static string SUBMITED_SURVEY = "E257";

        /// <summary>
        /// Vị trí submit đã tồn tại
        /// </summary>
        public static string POSITION_OF_SALARY = "E258";

        /// <summary>
        /// Bạn cần đăng nhập để thực hiện hành động này
        /// </summary>
        public static string YOU_MUST_LOGIN = "E402";
		/// <summary>
		/// Bạn không có quyền để thực hiện hành động này
		/// </summary>
		public static string YOU_NOT_HAVE_PERMISSION = "E403";
		/// <summary>
		/// Không tìm thấy
		/// </summary>
		public static string NOT_FOUND = "E404";
		/// <summary>
		/// Hệ thống bị lỗi, vui lòng thử lại sau.
		/// </summary>
		public static string SERVER_ERROR = "E500";
		/// <summary>
		/// Đã lưu dữ liệu thành công.
		/// </summary>
		public static string SAVE_DATA_SUCCESS = "S001";
		/// <summary>
		/// Đã xóa dữ liệu thành công.
		/// </summary>
		public static string DELETE_DATA_SUCCESS = "S002";
		/// <summary>
		/// Đã tạo file javascript thành công
		/// </summary>
		public static string CREATE_JS_SUCCESS = "S003";
		/// <summary>
		/// Đã build lại file chức năng hệ thống thành công
		/// </summary>
		public static string BUILD_FILE_SUCCESS = "S004";
		/// <summary>
		/// Đã khôi phục mật khẩu thành công
		/// </summary>
		public static string RESET_PASSWORD_SUCCESS = "S005";
		/// <summary>
		/// Đã nhắc nhở chuyển khoản cho đơn hàng thành công
		/// </summary>
		public static string REMIND_ORDER_TRANSFER_SUCCESS = "S006";
		/// <summary>
		/// Đã xác nhận đơn hàng đã thanh toán thành công
		/// </summary>
		public static string CONFIRM_ORDER_TRANSFER_SUCCESS = "S007";
		/// <summary>
		/// Đã sao chép dữ liệu thành công
		/// </summary>
		public static string CLONE_DATA_SUCCESS = "S008";
		/// <summary>
		/// Đã gửi thông báo thành công đến khách hàng
		/// </summary>
		public static string SEND_NOTIFICATION_SUCCESS = "S009";
		/// <summary>
		/// Đã gửi đến tất cả khách hàng thành công
		/// </summary>
		public static string SEND_POINT_OR_VOUCHER_TO_CUSTOMER_SUCCESS = "S010";
		/// <summary>
		/// Đã xác nhận đơn hàng thành công
		/// </summary>
		public static string CONFIRM_ORDER_SUCCESS = "S011";
		/// <summary>
		/// Đã xác nhận thanh toán đơn hàng thành công
		/// </summary>
		public static string PAID_ORDER_SUCCESS = "S012";
		/// <summary>
		/// Đã huỷ đơn hàng thành công
		/// </summary>
		public static string CANCEL_ORDER_SUCCESS = "S013";
		/// <summary>
		/// Đã xác nhận hoàn thành đơn hàng thành công
		/// </summary>
		public static string FINISH_ORDER_SUCCESS = "S014";
		/// <summary>
		/// Đã thay đổi trạng thái đơn hàng thành công
		/// </summary>
		public static string CHANGE_STATUS_ORDER_SUCCESS = "S015";
		/// <summary>
		/// Đã import dữ liệu từ file đã chọn thành công
		/// </summary>
		public static string IMPORT_SUCCESS = "S016";
		/// <summary>
		/// Đã gửi email thành công đến khách hàng theo dõi
		/// </summary>
		public static string SEND_EMAIL_PROMOTION_SUCCESS = "S017";
		/// <summary>
		/// Việc tạo lại file javascript này có thể gây ra một số thay đổi không mong muốn cho hệ thống, bạn có muốn tiếp tục không?
		/// </summary>
		public static string WARNING_CREATE_JS = "W001";
		/// <summary>
		/// Việc build lại file chức năng của hệ thống có thể gây ra một số thay đổi không mong muốn, và phải tiến hành build lại toàn bộ project, bạn có muốn tiếp tục không?
		/// </summary>
		public static string WARNING_BUILD_FILE = "W002";
		/// <summary>
		/// Bạn đã thay đổi link SEO của chương trình khuyến mãi này, nếu tiếp tục lưu tất cả dữ liệu SEO của chương trình này sẽ bị mất, bạn có muốn tiếp tục không ?
		/// </summary>
		public static string WARNING_CHANGE_PROMOTION_BEAUTYID = "W003";
		/// <summary>
		/// Mã giảm giá của bạn giảm trên 50%, bạn có muốn tiếp tục tạo mã giảm giá này không?
		/// </summary>
		public static string WARNING_VOUCHER_50_PERCENT = "W004";
		/// <summary>
		/// Dữ liệu sẽ được import từ file đã chọn, vui lòng đảm bảo dữ liệu trong file đống với file mẫu đã được cung cấp. Tiếp tục?
		/// </summary>
		public static string WARNING_IMPORT_FILE = "W005";
        /// <summary>
        /// Đã checkIn trong hôm nay
        /// </summary>
        public static string CHECKED_ERROR = "H001";
        /// <summary>
        /// Đã checkOut trong hôm nay
        /// </summary>
        public static string CHECKOUT_ERROR = "H002";
        /// <summary>
        /// Bạn chưa checkIn trong hôm nay, vui lòng checkIn trước khi checkOut
        /// </summary>
        public static string CHECKIN_ERROR = "H003";
		/// <summary>
        /// Thời gian checkIn không được muộn hơn thời gian kết thúc ca
        /// </summary>
		public static string CHECKIN_LATER = "H004";
		/// <summary>
        /// Thời gian checkOut không được sớm hơn thời gian bắt đầu ca
        /// </summary>
		public static string CHECKOUT_SOON = "H005";
        /// <summary>
        /// Ca làm việc đã tồn tại trong hệ thống
        /// </summary>
        public static string SHIFT_EXISTED = "H006";
		/// <summary>
		/// Thời gian kết thúc phải lớn hơn thời gian bắt đầu
		/// </summary>
		public static string DATE_ERROR = "H007";
        /// <summary>
        /// Tên quyền không được để trống
        /// </summary>
        public static string NAME_IS_EMPTY = "H008";
        /// <summary>
        /// Id quyền đã tồn tại
        /// </summary>
        public static string ID_PERMISSION_EXISTED = "H009";
		/// <summary>
        /// Quyền cha không tồn tại
        /// </summary>
		public static string ID_PERMISSION_PARENT_NOT_EXISTED = "H010";
		/// <summary>
        /// Quuyền hạn cha không cấp phép cho chức năng này
        /// </summary>
		public static string PERMISSION_PARENT_INCOMPETENT = "H011";
		/// <summary>
        /// Quyền hạn không tồn tại
        /// </summary>
		public static string PERMISSION_NOT_EXISTED = "H012";
        /// <summary>
        /// Tên quyền kiểu nghỉ không được để trống
        /// </summary>
        public static string NAME_VACATION_IS_EMPTY = "H013";
        /// <summary>
        /// Kiểu nghỉ không tồn tại
        /// </summary>
        public static string VACATION_IS_NOT_EXITED = "H014";
        /// <summary>
        /// Vui lòng sắp xếp ca trước khi checkIn
        /// </summary>
        public static string ARRANGE_SHIFT_NOT_EXITED  = "H015";
        /// <summary>
        /// Không tìm thấy ca của bạn để checkIn
        /// </summary>
        public static string NOT_FIND_SHIFT_OF_YOU = "H016";
		/// <summary>
        /// Hôm nay bạn đã xin nghỉ cả ngày, vui lòng kiểm tra lại
        /// </summary>
		public static string HAVE_EXTIED_PAID_LEAVE = "H017";
        /// <summary>
        /// Bạn chưa checkOut trong hôm nay, vui lòng checkOut để tiếp tục checkIn ca mới
        /// </summary>
        public static string NOT_CHECKOUT = "H018";
        /// <summary>
        /// Hôm nay là ngày nghỉ, vui lòng kiểm tra lại
        /// </summary>
        public static string TODAY_IS_DAY_OFF = "H019";
        /// <summary>
        /// Không tìm thấy đơn tăng ca đã duyệt trong ngày của bạn, vui lòng kiểm tra lại
        /// </summary>
        public static string OVERTIME_NOT_FIND = "H020";

        /// <summary>
        /// Mã nhân viên đã tồn tại trong hệ thống
        /// </summary>
        public static string IDENTITYCARD_ID_EXISTING = "E167";
        /// <summary>
        /// Mã định khoản đã tồn tại
        /// </summary>
        public static string ACCOUNTITEM_CODE_EXISTING = "E178";
        /// <summary>
        /// Số lượng sản phẩm tồn không đúng
        /// </summary>
        public static string INVENTORY_QUANTITY = "E230";
	   		/// <summary>
        /// Số lượng sản phẩm nhập lớn hơn số lượng sản phẩm mua
        /// </summary>
        public static string PURCHASE_QUANTITY = "E238";
	   /// <summary>
        /// Phiếu mua hàng đã hoàn thành
        /// </summary>
        public static string PURCHASE_FINISH = "E251";

		/// <summary>
        /// Phiếu thu/chi với HĐBH này đã tồn tại.
        /// </summary>
        public static string RECEIPT_EXPENSE_VOUCHER_WITH_ORDER_ALREADY_EXISTS = "E256";
		/// <summary>
        /// Sản phẩm khuyến mãi không hợp lệ
        /// </summary>
        public static string DEAL_SHOCK_PRODUCT_NOT_VALID = "E257";

		/// <summary>
        /// Không thể cập nhật đơn hàng do đơn hàng chưa được thanh toán thành công
        /// </summary>
        public static string ORDER_PAID_NOT_VALID = "E258";

		/// <summary>
        /// Chưa hoàn thành tất cả đơn kiểm
        /// </summary>
        public static string UNFINISHED_INVENTORY = "E259";

		/// <summary>
        /// Không tìm thấy phiếu kiểm nào
        /// </summary>
        public static string NOT_FOUND_INVENTORY = "E260";

		/// <summary>
        /// Phiếu kiểm kho gộp đã hoàn thành
        /// </summary>
        public static string INVENTORY_AGGREGATE_FINISH = "E261";

        /// <summary>
        /// Số tiền ứng trước lương vượt quá giới hạn cho phép
        /// </summary>
        public static string ADVANCE_VALUE_EXCEED_LIMIT = "E262";

        /// <summary>
        /// Chưa đồng ý với điều khoản.
        /// </summary>
        public static string NOT_AGREE_TERMS = "E263";

        /// <summary>
        /// Không tìm thấy Kỳ lương phù hợp.
        /// </summary>
        public static string PAYROLL_NOT_FOUND = "E264";

        /// <summary>
        /// Kỳ lương chưa được duyệt.
        /// </summary>
        public static string PAYROLL_NOT_APPROVED = "E265";

        /// <summary>
        /// Các bảng chi trả lương không cùng một trạng thái.
        /// </summary>
        public static string SALARY_PAYMENTS_STATUS_INVALID = "E266";

        /// <summary>
        /// Trạng thái cần cập nhật không hợp lệ.
        /// </summary>
        public static string SALARY_PAYMENT_STATUS_INVALID = "E267";

		/// <summary>
        /// Bảng chi trả lương gần nhất của kỳ lương này chưa được chi trả.
        /// </summary>
        public static string SALARY_PAYMENT_UNPAID = "E268";

		/// <summary>
        /// Tổng tỉ lệ thanh toán của các đợt chi trả lương phải nhỏ hơn 100%.
        /// </summary>
        public static string MAX_SALARY_PAYMENT_RATE_EXCEED = "E269";

		/// <summary>
        /// Mã chứng từ hoặc mã tài sản đã tồn tại.
        /// </summary>
        public static string FINANCIAL_CODE_OR_ASSET_CODE_IS_EXISTED = "E270";

		/// <summary>
        /// Không tìm thấy phòng ban.
        /// </summary>
        public static string DEPARTMENT_NOT_FOUND = "E271";

		/// <summary>
        /// Không tìm thấy người quản lý.
        /// </summary>
        public static string USER_NOT_FOUND = "E272";

        /// <summary>
        /// Không tìm thấy tài sản.
        /// </summary>
        public static string ASSET_NOT_FOUND = "E273";
        /// <summary>
        /// Tên đơn vị tính đã tồn tại.
        /// </summary>
        public static string CALCULATION_UNIT_NAME_IS_EXISTED = "E274";

        /// <summary>
        /// Không tìm thấy đơn vị tính.
        /// </summary>
        public static string CALCULATION_UNIT_NOT_FOUND = "E275";

        /// <summary>
        /// Đã tồn tại mã khu vực
        /// </summary>
        public static string EXIST_AREA_CODE = "E276";

        /// <summary>
        /// Vùng miền của khu vực không hợp lệ
        /// </summary>
        public static string AREA_REGION_NOT_FOUND = "E277";

        /// <summary>
        /// Người quản lý không tồn tại
        /// </summary>
        public static string AREA_MANAGER_NOT_FOUND = "E278";

		/// <summary>
        /// Không tìm thấy tài sản trong kho
        /// </summary>
        public static string ASSET_STOCK_NOT_FOUND = "E279";

        /// <summary>
        /// Số lượng xuất vượt quá số lượng tài sản còn lại trong kho
        /// </summary>
        public static string OUTPUT_QUANTITY_EXCEED_ASSET_IN_STOCK = "E280";

        /// <summary>
        /// Đang được sử dụng
        /// </summary>
        public static string IN_USE = "E281";

        /// <summary>
        /// Data không khớp
        /// </summary>
        public static string DATA_NOT_MATCH = "E282";

        /// <summary>
        /// Tất cả các mục đã được chọn
        /// </summary>
        public static string ALL_ITEMS_HAVE_BEEN_SELECTED = "E283";

        /// <summary>
        /// Không thuộc vị trí áp dụng khấu trừ
        /// </summary>
        public static string NOT_IN_THE_LOCATION_THE_DEDUCTION_APPLY = "E284";
        
        /// <summary>
        /// Trạng thái chi nhánh không hợp lệ.
        /// </summary>
        public static string BRANCH_STATUS_INVALID = "E285";
        
        /// <summary>
        /// Khu vực không tồn tại
        /// </summary>
        public static string AREA_NOT_FOUND = "E286";
        
        /// <summary>
        /// Chi nhánh vẫn còn hàng.
        /// </summary>
        public static string BRANCH_EXIST_INVENTORY = "E287";

		/// <summary>
		/// Không tìm thấy loại tài sản
		/// </summary>
		public static string TYPE_OF_ASSET_NOT_FOUND = "E288";
		
		/// <summary>
		/// Không tìm thấy loại tài sản cha
		/// </summary>
		public static string PARENT_TYPE_OF_ASSET_NOT_FOUND = "E289";
		
		/// <summary>
		/// Tên loại tài sản đã tồn tại
		/// </summary>
		public static string NAME_OF_ASSET_TYPE_ALREADY_EXIST = "E290";
		
		/// <summary>
		/// Mã loại tài sản đã tồn tại
		/// </summary>
		public static string CODE_OF_ASSET_TYPE_ALREADY_EXIST = "E291";
        
        /// <summary>
        /// Tồn tại chi nhánh ở trong khu vực
        /// </summary>
        public static string AREA_EXIST_BRANCH = "E292";
        
        /// <summary>
        /// Tồn tại khu vực trong vùng miền
        /// </summary>
        public static string REGION_EXIST_AREA = "E293";

        /// <summary>
        /// Không tìm thấy phòng ban nhân viên bàn giao tài sản
        /// </summary>
        public static string USER_EXPORT_DEPARTMENT_NOT_FOUND = "E294";

        /// <summary>
        /// Không tìm thấy phòng ban nhân viên được bàn giao tài sản
        /// </summary>
        public static string USER_IMPORT_DEPARTMENT_NOT_FOUND = "E295";

        /// <summary>
        /// Không tìm thấy chi nhánh của nhân viên bàn giao tài sản
        /// </summary>
        public static string USER_EXPORT_BRANCH_NOT_FOUND = "E296";

        /// <summary>
        /// Không tìm thấy chi nhánh nhân viên được bàn giao tài sản
        /// </summary>
        public static string USER_IMPORT_BRANCH_NOT_FOUND = "E297";
        /// <summary>
        /// Trạng thái bảng lương cần xoá không hợp lệ
        /// </summary>
        public static string DELETE_PAYROLL_STATUS_INVALID = "E298";
		/// <summary>
		/// Vẫn còn tài sản thuộc loại/nhóm tài sản
		/// </summary>
		public static string STILL_HAS_EXISTING_ASSETS = "E299";
		/// <summary>
		/// Không tìm thấy nhóm tài sản
		/// </summary>
		public static string GROUP_OF_ASSET_NOT_FOUND = "E300";
		/// <summary>
		/// Không tìm thấy nhóm tài sản cha
		/// </summary>
		public static string PARENT_GROUP_OF_ASSET_NOT_FOUND = "E301";
		/// <summary>
		/// Tên nhóm tài sản đã tồn tại
		/// </summary>
		public static string NAME_OF_ASSET_GROUP_ALREADY_EXIST = "E302";
		/// <summary>
		/// Mã nhóm tài sản đã tồn tại
		/// </summary>
		public static string CODE_OF_ASSET_GROUP_ALREADY_EXIST = "E303";
		/// <summary>
		/// Không thể tạo thêm danh mục con
		/// </summary>
		public static string CAN_NOT_CREATE_MORE_CHILD = "E304";

        /// <summary>
        /// Không thể thay đổi phương thức thanh toán cho đơn hàng trả toàn phần.
        /// </summary>
        public static string CAN_NOT_CHANGE_PAYMENT_METHOD_RETURN_IN_FULL = "E305";

		/// <summary>
		/// Mã đơn vị tính đã tồn tại
		/// </summary>
		public static string CALCULATION_UNIT_CODE_IS_EXISTED = "E306";
		/// <summary>
		/// Không có tài sản trong phiếu bàn giao
		/// </summary>
		public static string NO_ASSET_FOR_HANDOVER = "E307";

		/// <summary>
		/// Tài sản đã được thay đổi số lượng
		/// </summary>
		public static string ASSET_QUANTITY_ALREADY_CHANGED = "E308";

		/// <summary>
    	/// Không thể được cập nhật vào danh mục con
    	/// </summary>
    	public static string CANNOT_BE_UPDATED_TO_CHILD = "E309";
	
		/// <summary>
        /// Trạng thái phiếu ứng lương không hợp lệ.
        /// </summary>
        public static string SALARY_ADVANCE_STATUS_INVALID = "E310";

		/// <summary>
        /// Thời gian áp dụng không hợp lệ.
        /// </summary>
        public static string EMPLOYEE_SALARY_TIME_APPLY_INVALID = "E311";
        /// <summary>
		/// Vị trí không tồn tại
		/// </summary>
		public static string POSITION_NOT_FOUND = "E312";

        /// <summary>
        /// Loại tăng ca không tồn tại.
        /// </summary>
        public static string TYPE_OVERTIME_SALARY_NOT_FOUND = "E313";

        
        /// <summary>
        /// Dữ liệu trong file không đúng định dạng.
        /// </summary>
        public static string FILE_DATA_NOT_VALID = "E314";
		/// <summary>
		/// Đơn vị tính vẫn đang được sử dụng.
		/// </summary>
		public static string CALCULATION_UNIT_ALREADY_IN_USE = "E315";

        /// <summary>
		/// Nhân viên không tồn tại.
		/// </summary>
        public static string EMPLOYEE_NOT_EXIST = "E316";

        /// <summary>
        /// Số lượng sản phẩm tại vị trí vượt quá số lượng tồn kho
        /// </summary>
        public static string POSITION_PRODUCT_QUANTITY_EXCEED_INVENTORY = "E317";

        /// <summary>
        /// Trạng thái dữ liệu doanh thu không hợp lệ
        /// </summary>
        public static string PAYROLL_DATA_STATUS_INVALID = "E318";
		/// <summary>
		/// Mã kiểm kê đã tồn tại
		/// </summary>
		public static string INVENTORY_CHECK_CODE_IS_ALREADY_EXIST = "E319";
		/// <summary>
		/// Danh sách tài sản kiểm kê đang rỗng
		/// </summary>
		public static string INVENTORY_CHECK_RECORD_IS_EMPTY = "E320";
		/// <summary>
		/// Phiếu kiểm kê không tồn tại
		/// </summary>
		public static string INVENTORY_CHECK_NOT_FOUND = "E321";
		/// <summary>
		/// Dữ liệu năng suất tháng đã tạo
		/// </summary>
		public static string PRODUCTIVITY_IS_CREATED = "E322";
		/// <summary>
		/// Loại lương không hợp lệ
		/// </summary>
		public static string SALARY_TYPE_INVALID = "E323";
		/// <summary>
		/// Mục tài sản đang rỗng
		/// </summary>
		public static string ASSET_CATEGORY_CODE_IS_EMPTY = "E324";
		/// <summary>
		/// Loại hoặc nhóm tài sản không hợp lệ (Không được sử dụng loại cha)
		/// </summary>
		public static string INVALID_ASSET_TYPE_OR_GROUP = "E325";
        /// <summary>
        /// Không tìm thấy thẻ tag (đoạn chat)
        /// </summary>
        public static string NOT_FOUND_CONVENTION_TAG = "E326";
		/// <summary>
		/// Tài khoản kế toán không được trống
		/// </summary>
		public static string ACCOUNTING_ACCOUNT_IS_EMPTY = "E327";
		/// <summary>
		/// Tài khoản kế toán không tồn tại
		/// </summary>
		public static string ACCOUNTING_ACCOUNT_NOT_EXIST = "E328";
        /// <summary>
        /// Tin nhắn không tồn tại
        /// </summary>
        public static string NOT_FOUNT_MESSAGE = "E329";
		/// <summary>
		/// Bậc lương vẫn đang được sử dụng
		/// </summary>
		public static string SALARY_LEVEL_DETAIL_IN_USE = "E330";
		/// <summary>
		/// Không tìm thấy cấp lương
		/// </summary>
		public static string SALARY_LEVEL_NOT_FOUND = "E331";
		/// <summary>
		/// Không tìm thấy thành phần lương
		/// </summary>
		public static string SALARY_COMPONENT_NOT_FOUND = "E333";
    }
}
