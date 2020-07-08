
// ZebraCameraSDKSampleApplicationDlg.cpp : implementation file
//

#include "stdafx.h"
#include "zebra_camera_sdk_sample_application.h"
#include "zebra_camera_sdk_sample_application_dlg.h"
#include "afxdialogex.h"
#include "zebra_camera.h"
#include "zebra_camera_manager.h"

#include <Windows.h>
#include <algorithm>
#include <memory>
#include <strsafe.h>
#include <future>
#include <fstream>
#include <iostream>
#include <time.h>
#include <math.h>
#include <filesystem>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

int ReadCustomPID();

// CAboutDlg dialog used for App About
class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// constants
#define WM_CAMERA_UPDATE_NOTIFY 100
#define WM_CAMERA_ATTACHED_NOTIFY 200
#define WM_CAMERA_DETACHED_NOTIFY 201
#define WM_CAMERA_DECODE_SESSION_STATUS_NOTIFY 202
#define VENDOR_ID 1504
#define PRODUCT_ID 2213
#define IMAGE_SIZE_PRECISION 3
#define IMAGE_SIZE_KB_DEVISOR 1024.0
#define IMAGE_NAME_PREFIX "image_"
#define IMAGE_SAVE_FOLDER_DEFAULT "ZebraCamera"
#define IMAGE_EXT_BMP ".bmp"
#define IMAGE_EXT_JPEG ".jpeg"
#define STR_IMAGE_EVTENT_CONTINUOUS "Continuous Image"
#define STR_IMAGE_EVTENT_SNAPSHOT "Snapshot Image"
#define STR_IMAGE_EVTENT_PRODUCE "Produce Image"
#define STR_IMAGE_EVTENT_DECODE "Decode Image"
#define STR_NOT_APPLICABLE "N/A"
#define INVALID_INDEX -1
#define STR_ERROR _T("Error")
#define STR_ERROR_MSG_SAVE_LOCATION_NOT_SET _T("Please set the save location")
#define STR_ERROR_MSG_INVALID_SAVE_LOCATION _T("Invalid folder location")
#define STR_ERROR_MSG_IMAGE_EVENTS_NOT_ENABLED _T("Image events not enabled")
#define STR_ERROR_MSG_INVALID_FIRMWARE_FILE _T("Invalid firmware file")
#define STR_DIALOG_TITLE_SELECT_IMAGE_SAVE_FOLDER _T("Please select a folder for storing Images :")
#define STR_DIALOG_TITLE_SELECT_FIRMWARE_FILE _T("Select a firmware file")
#define STR_DIALOG_TITLE_LOAD_CONFIGURATION "Load configuration"
#define STR_DIALOG_TITLE_RETIEVE_CONFIGURATION "Retrieve configuration"
#define STR_ILLUMINATION_MODE_STANDARD _T("0=Standard")
#define STR_ILLUMINATION_MODE_ON _T("1=On")
#define STR_ILLUMINATION_MODE_OFF _T("2=Off")
#define STR_VIDEO_MODE_OFF _T("0=Off")
#define STR_VIDEO_MODE_DEVICE_WAKEUP _T("1=Device wakeup")
#define STR_VIDEO_MODE_CONTINUOUS _T("2=Continuous")
#define STR_IMAGE_FORMAT_CONVERT_TO_BMP _T("Convert to BMP")
#define STR_IMAGE_FORMAT_CONVERT_TO_JPEG _T("Convert to JPEG")
#define STR_IMAGE_FORMAT_BMP _T("BMP")
#define STR_IMAGE_FORMAT_JPEG _T("JPEG")
#define COMBO_ITEM_NOT_SELECTED_VALUE -1
#define STR_POWER_USER_MODE_ENABLE _T("Enable")
#define STR_POWER_USER_MODE_DISABLE _T("Disable")
#define WHITE_BALANCE_COMPONENT_MAX 0X4000
#define FLOATING_POINT 12
#define VIDEO_MODE_CONTINUOUS 2
#define VIDEO_MODE_WAKE_UP 1
#define DELAY_BEFORE_ACCESS_EXTENSION_PROPERTIES 250
#define CAMERA_AWAKE 1
#define CAMERA_ASLEEP 0


// Constructor
CZebraCameraSDKSampleApplicationDlg::CZebraCameraSDKSampleApplicationDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_ZEBRACAMERASDKSAMPLEAPPLICATION_DIALOG, pParent)
	, m_edit_SerialNo_value_(_T(""))
	, m_slider_brightness_value_(0)
	, m_edit_brightness_value_(_T(""))
	, m_slider_contrast_value_(0)
	, m_edit_contrast_value_(_T(""))
	, m_slider_saturation_value_(0)
	, m_edit_saturation_value_(_T(""))
	, m_slider_sharpness_value_(0)
	, m_edit_sharpness_value_(_T(""))
	, m_slider_wb_component_blue_value_(0)
	, m_edit_whitebalance_value_(_T(""))
	, m_cbox_whitebalanceAuto_value_(FALSE)
	, m_edit_modelnumber_value_(_T(""))
	, m_edit_dateofmanufacture_value_(_T(""))
	, m_edit_dateofservice_value_(_T(""))
	, m_edit_dateoffirstprogram_value_(_T(""))
	, m_edit_firmwareversion_value_(_T(""))
	, m_edit_hardwareversion_value_(_T(""))
	, m_check_autosaveimage_value_(FALSE)
	, m_button_saveButtonPressed_(FALSE)
	, m_check_picklist_image_value_(FALSE)
	, m_check_snapshot_image_event_value_(FALSE)
	, m_check_decode_image_event_value_(FALSE)
	, m_check_continuous_image_event_value_(FALSE)
	, m_edit_eventType_value_(_T(""))
	, m_edit_size_value_(_T(""))
	, m_edit_format_value_(_T(""))
	, m_edit_timestamp_value_(_T(""))
	, m_edit_saveLocation_value_(_T(""))
	, m_check_white_balance_temp_auto_value_(FALSE)
	, m_edit_fwFilePath_value_(_T(""))
	, m_edit_exposure_value_(_T(""))
	, m_slider_exposure_value_(0)
	, m_check_exposure_auto_value_(FALSE)
	, m_edit_image_resolution_value_(_T(""))
	, m_edit_decode_data_value_(_T(""))
	, m_slider_gamma_value_(0)
	, m_edit_gamma_value_(_T(""))
	, m_slider_gain_value_(0)
	, m_edit_gain_value_(_T(""))
	, m_slider_backlight_comp_value_(0)
	, m_edit_backlight_comp_value_(_T(""))
	, m_slider_wb_component_red_value_(0)
	, m_edit_wb_component_red_value_(_T(""))
	, m_edit_wb_blue_temp_value_(_T(""))
	, m_edit_wb_red_temp_value_(_T(""))
	, m_detect_bounding_box_(FALSE)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	auto pid = ReadCustomPID();

	continuous_image_event_handler_ = new ContinuousImageEventHandler(this);
	snapshot_image_event_handler_ = new SnapshotImageEventHandler(this);
	produce_image_event_handler_ = new ProduceImageEventHandler(this);
	decode_image_event_handler_ = new DecodeImageEventHandler(this);
	decode_session_status_change_event_handler_ = new DecodeSessionStatusChangeEventHandler(this);
	firmware_download_event_handler_ = new FirmwareDownloadEventHandler(this);

	m_device_awake_bmp_.LoadBitmapW(IDB_DEVICE_AWAKE);
	m_device_sleep_bmp_.LoadBitmapW(IDB_DEVICE_SLEEP);

}

CZebraCameraSDKSampleApplicationDlg::~CZebraCameraSDKSampleApplicationDlg()
{
	delete continuous_image_event_handler_;
	delete snapshot_image_event_handler_;
	delete produce_image_event_handler_;
	delete decode_image_event_handler_;
	delete decode_session_status_change_event_handler_;
	delete firmware_download_event_handler_;
	m_device_sleep_bmp_.DeleteObject();
	m_device_awake_bmp_.DeleteObject();
}

// Dialog Data exchange event
void CZebraCameraSDKSampleApplicationDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_SERIAL_NO, m_edit_SerialNo_value_);
	DDX_Control(pDX, IDC_EDIT_BRIGHTNESS, m_edit_brightness_);
	DDX_Text(pDX, IDC_EDIT_BRIGHTNESS, m_edit_brightness_value_);
	DDX_Control(pDX, IDC_SLIDER_BRIGHTNESS, m_slider_brightness_);
	DDX_Slider(pDX, IDC_SLIDER_BRIGHTNESS, m_slider_brightness_value_);
	DDV_MinMaxInt(pDX, m_slider_brightness_value_, 0, 100);
	DDX_Control(pDX, IDC_SLIDER_CONTRAST, m_slider_contrast_);
	DDX_Slider(pDX, IDC_SLIDER_CONTRAST, m_slider_contrast_value_);
	DDX_Control(pDX, IDC_EDIT_CONTRAST, m_edit_contrast_);
	DDX_Text(pDX, IDC_EDIT_CONTRAST, m_edit_contrast_value_);
	DDX_Control(pDX, IDC_SLIDER_SATURATION, m_slider_saturation_);
	DDX_Slider(pDX, IDC_SLIDER_SATURATION, m_slider_saturation_value_);
	DDX_Control(pDX, IDC_EDIT__SATURATION, m_edit_saturation_);
	DDX_Text(pDX, IDC_EDIT__SATURATION, m_edit_saturation_value_);
	DDX_Control(pDX, IDC_SLIDER_SHARPNESS, m_slider_sharpness_);
	DDX_Slider(pDX, IDC_SLIDER_SHARPNESS, m_slider_sharpness_value_);
	DDX_Control(pDX, IDC_EDIT_SHARPNESS, m_edit_sharpness_);
	DDX_Text(pDX, IDC_EDIT_SHARPNESS, m_edit_sharpness_value_);
	DDX_Control(pDX, IDC_SLIDER_WHITEBALANCE, m_slider_wb_component_blue_);
	DDX_Slider(pDX, IDC_SLIDER_WHITEBALANCE, m_slider_wb_component_blue_value_);
	DDX_Control(pDX, IDC_SLIDER_WB_RED, m_slider_wb_component_red_);
	DDX_Slider(pDX, IDC_SLIDER_WB_RED, m_slider_wb_component_red_value_);

	DDX_Control(pDX, IDC_EDIT_WB, m_edit_whitebalance_);
	DDX_Text(pDX, IDC_EDIT_WB, m_edit_whitebalance_value_);
	DDX_Control(pDX, IDC_EDIT_SERIAL_NO, m_edit_serialNo_);
	DDX_Control(pDX, IDC_EDIT_MODEL_NO, m_edit_modelnumber_);
	DDX_Text(pDX, IDC_EDIT_MODEL_NO, m_edit_modelnumber_value_);
	DDX_Control(pDX, IDC_EDIT_DATE_OF_MANUFACTURE, m_edit_dateofmanufacture_);
	DDX_Text(pDX, IDC_EDIT_DATE_OF_MANUFACTURE, m_edit_dateofmanufacture_value_);
	DDX_Control(pDX, IDC_EDIT_DATE_OF_SERVICE, m_edit_dateofservice_);
	DDX_Text(pDX, IDC_EDIT_DATE_OF_SERVICE, m_edit_dateofservice_value_);
	DDX_Control(pDX, IDC_EDIT_DATE_OF_FIRSTPROGRAM, m_edit_dateoffirstprogram_);
	DDX_Text(pDX, IDC_EDIT_DATE_OF_FIRSTPROGRAM, m_edit_dateoffirstprogram_value_);
	DDX_Control(pDX, IDC_EDIT_FIRMWARE_VERSION, m_edit_firmwareversion_);
	DDX_Text(pDX, IDC_EDIT_FIRMWARE_VERSION, m_edit_firmwareversion_value_);
	DDX_Control(pDX, IDC_EDIT_HARDWARE_VERSION, m_edit_hardwareversion_);
	DDX_Text(pDX, IDC_EDIT_HARDWARE_VERSION, m_edit_hardwareversion_value_);
	DDX_Control(pDX, IDC_BUTTON_SETDEFAULTS, m_button_setdefaults_);
	DDX_Control(pDX, IDC_BUTTON_REBOOT, m_button_reboot_);
	DDX_Control(pDX, IDC_BUTTON_CAPTURE, m_button_capture_);
	DDX_Control(pDX, IDC_BUTTON_BROWSE, m_button_browse_);
	DDX_Control(pDX, IDC_BUTTON_SAVE_IMAGE, m_button_save_);
	DDX_Control(pDX, IDC_CHECK_AUTO_SAVE_IMAGE, m_check_autosaveimage_);
	DDX_Check(pDX, IDC_CHECK_AUTO_SAVE_IMAGE, m_check_autosaveimage_value_);
	DDX_Control(pDX, IDC_STATIC_IMAGE_VIEW, m_picturecontrol_image_);
	DDX_Control(pDX, IDC_CHECK_PICKLIST_IMAGE_EVENT, m_check_picklist_image_);
	DDX_Check(pDX, IDC_CHECK_PICKLIST_IMAGE_EVENT, m_check_picklist_image_value_);
	DDX_Control(pDX, IDC_CHECK_SNAPSHOT_IMAGE_EVENT, m_check_snapshot_image_event_);
	DDX_Check(pDX, IDC_CHECK_SNAPSHOT_IMAGE_EVENT, m_check_snapshot_image_event_value_);
	DDX_Control(pDX, IDC_CHECK_DECODE_IMAGE_EVENT, m_check_decode_image_event_);
	DDX_Check(pDX, IDC_CHECK_DECODE_IMAGE_EVENT, m_check_decode_image_event_value_);
	DDX_Control(pDX, IDC_CHECK_CONTINUOUS_IMAGE_EVENT, m_check_continuous_image_event_);
	DDX_Check(pDX, IDC_CHECK_CONTINUOUS_IMAGE_EVENT, m_check_continuous_image_event_value_);
	DDX_Control(pDX, IDC_EDIT_EVENT_TYPE, m_edit_eventType_);
	DDX_Text(pDX, IDC_EDIT_EVENT_TYPE, m_edit_eventType_value_);
	DDX_Control(pDX, IDC_EDIT_SIZE, m_edit_size_);
	DDX_Text(pDX, IDC_EDIT_SIZE, m_edit_size_value_);
	DDX_Control(pDX, IDC_EDIT_FORMAT, m_edit_format_);
	DDX_Text(pDX, IDC_EDIT_FORMAT, m_edit_format_value_);
	DDX_Control(pDX, IDC_EDIT_TIMESTAMP, m_edit_timestamp_);
	DDX_Text(pDX, IDC_EDIT_TIMESTAMP, m_edit_timestamp_value_);
	DDX_Control(pDX, IDC_EDIT_SAVE_LOCATION, m_edit_saveLocation_);
	DDX_Text(pDX, IDC_EDIT_SAVE_LOCATION, m_edit_saveLocation_value_);
	DDX_Control(pDX, IDC_EDIT_FW_FILE_PATH, m_edit_fwFilePath_);
	DDX_Text(pDX, IDC_EDIT_FW_FILE_PATH, m_edit_fwFilePath_value_);
	DDX_Control(pDX, IDC_BUTTON_FW_BROWSE, m_button_fwFileBrowse_);
	DDX_Control(pDX, IDC_BUTTON_FW_DOWNLOAD, m_button_fwDownload_);
	DDX_Control(pDX, IDC_BUTTON_FW_INSTALL, m_button_fwInstall_);
	DDX_Control(pDX, IDC_PROGRESS_FIRMWARE_DOWNLOAD, m_progressbar_fwDownload_);
	DDX_Control(pDX, IDC_BUTTON_FW_CANCEL, m_button_fwCancel_);
	DDX_Control(pDX, IDC_CHECK_WHITEBALANCETEMP_AUTO, m_check_white_balance_temp_auto_);
	DDX_Check(pDX, IDC_CHECK_WHITEBALANCETEMP_AUTO, m_check_white_balance_temp_auto_value_);
	DDX_Control(pDX, IDC_EDIT_EXPOSURE, m_edit_exposure_);
	DDX_Text(pDX, IDC_EDIT_EXPOSURE, m_edit_exposure_value_);
	DDX_Control(pDX, IDC_SLIDER_EXPOSURE, m_slider_exposure_);
	DDX_Slider(pDX, IDC_SLIDER_EXPOSURE, m_slider_exposure_value_);
	DDX_Control(pDX, IDC_CHECK_EXPOSURE_AUTO, m_check_exposure_auto_);
	DDX_Check(pDX, IDC_CHECK_EXPOSURE_AUTO, m_check_exposure_auto_value_);
	DDX_Control(pDX, IDC_EDIT_IMAGE_RESOLUTION, m_edit_imge_resolution_);
	DDX_Text(pDX, IDC_EDIT_IMAGE_RESOLUTION, m_edit_image_resolution_value_);

	DDX_Control(pDX, IDC_EDIT_DECODE_DATA, m_edit_decode_data_);
	DDX_Text(pDX, IDC_EDIT_DECODE_DATA, m_edit_decode_data_value_);

	DDX_Control(pDX, IDC_SLIDER_GAMMA, m_slider_gamma_);
	DDX_Slider(pDX, IDC_SLIDER_GAMMA, m_slider_gamma_value_);
	DDX_Control(pDX, IDC_EDIT_GAMMA, m_edit_gamma_);
	DDX_Text(pDX, IDC_EDIT_GAMMA, m_edit_gamma_value_);

	DDX_Control(pDX, IDC_SLIDER_GAIN, m_slider_gain_);
	DDX_Slider(pDX, IDC_SLIDER_GAIN, m_slider_gain_value_);
	DDX_Control(pDX, IDC_EDIT_GAIN, m_edit_gain_);
	DDX_Text(pDX, IDC_EDIT_GAIN, m_edit_gain_value_);
	DDX_Control(pDX, IDC_BUTTON_WRITE_TO_FLASH, m_button_write_to_flash_);
	DDX_Control(pDX, IDC_SLIDER_BACKLIGHT_COMP, m_slider_backlight_comp_);
	DDX_Slider(pDX, IDC_SLIDER_BACKLIGHT_COMP, m_slider_backlight_comp_value_);
	DDX_Control(pDX, IDC_EDIT_BACKLIGHT_COMP, m_edit_backlight_comp_);
	DDX_Text(pDX, IDC_EDIT_BACKLIGHT_COMP, m_edit_backlight_comp_value_);

	DDX_Control(pDX, IDC_EDIT_WB_RED, m_edit_wb_component_red_);
	DDX_Text(pDX, IDC_EDIT_WB_RED, m_edit_wb_component_red_value_);
	DDX_Text(pDX, IDC_EDIT_wb_blue_temp, m_edit_wb_blue_temp_value_);
	DDX_Text(pDX, IDC_EDIT_wb_red_temp, m_edit_wb_red_temp_value_);
	DDX_Control(pDX, IDC_BUTTON_wb_get, m_button_wbc_get_temp_);
	DDX_Control(pDX, IDC_BUTTON_wb_set, m_button_wbc_set_temp_);
	DDX_Control(pDX, IDC_EDIT_wb_blue_temp, m_edit_wb_blue_temp_);
	DDX_Control(pDX, IDC_EDIT_wb_red_temp, m_edit_wb_red_temp_);
	DDX_Control(pDX, IDC_BUTTON_CLEAR_LOG, m_button_clear_log_);
	DDX_Control(pDX, IDC_EDIT_EVENT_LOG, m_edit_log_);
	DDX_Text(pDX, IDC_EDIT_EVENT_LOG, m_edit_log_value_);

	DDX_Control(pDX, IDC_BUTTON_LOAD_CONFIG, m_button_load_config_);
	DDX_Control(pDX, IDC_BUTTON_RETRIEVE_CONFIG, m_button_retrieve_config_);
	DDX_Control(pDX, IDC_CHECK_DECODE_SESSION_STATUS_CHANGE_EVENT, m_check_decode_session_status_change_event_);
	DDX_Check(pDX, IDC_CHECK_DECODE_SESSION_STATUS_CHANGE_EVENT, m_check_decode_session_status_change_event_value_);
	DDX_Control(pDX, IDC_PICTURE_DEVICE_AWAKE_STATUS, m_device_awake_status_image_);

	m_edit_log_.LineScroll(m_edit_log_.GetLineCount());
	DDX_Check(pDX, IDC_CHECK_DETECT_BOUNDING_BOX, m_detect_bounding_box_);
	DDX_Control(pDX, IDC_CHECK_DETECT_BOUNDING_BOX, m_check_detect_bounding_box_);
	DDX_Control(pDX, IDC_BUTTON_SET_BACKGROUND, m_button_set_background_);
}

BEGIN_MESSAGE_MAP(CZebraCameraSDKSampleApplicationDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_HSCROLL()
	ON_BN_CLICKED(IDC_BUTTON_SETDEFAULTS, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSetdefaults)
	ON_BN_CLICKED(IDC_BUTTON_REBOOT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonReboot)
	ON_BN_CLICKED(IDC_BUTTON_CAPTURE, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonCapture)
	ON_CBN_SELCHANGE(IDC_COMBO_CAMERA_LIST, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboCameraList)
	ON_MESSAGE(WM_APP + WM_CAMERA_UPDATE_NOTIFY, &CZebraCameraSDKSampleApplicationDlg::OnUpdateData)
	ON_MESSAGE(WM_APP + WM_CAMERA_ATTACHED_NOTIFY, &CZebraCameraSDKSampleApplicationDlg::OnCameraAttached)
	ON_MESSAGE(WM_APP + WM_CAMERA_DETACHED_NOTIFY, &CZebraCameraSDKSampleApplicationDlg::OnCameraDetached)
	ON_MESSAGE(WM_APP + WM_CAMERA_DECODE_SESSION_STATUS_NOTIFY, &CZebraCameraSDKSampleApplicationDlg::UpdateDecodeSessionStatusImage)
	ON_BN_CLICKED(IDC_BUTTON_BROWSE, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonBrowse)
	ON_BN_CLICKED(IDC_BUTTON_SAVE_IMAGE, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSaveImage)
	ON_BN_CLICKED(IDC_CHECK_AUTO_SAVE_IMAGE, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckAutoSaveImage)
	ON_BN_CLICKED(IDC_CHECK_CONTINUOUS_IMAGE_EVENT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckContinuousImageEvent)
	ON_BN_CLICKED(IDC_CHECK_SNAPSHOT_IMAGE_EVENT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckSnapshotImageEvent)
	ON_BN_CLICKED(IDC_CHECK_PICKLIST_IMAGE_EVENT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckProduceImageEvent)
	ON_BN_CLICKED(IDC_CHECK_DECODE_IMAGE_EVENT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDecodeImageEvent)
	ON_WM_CLOSE()
	ON_BN_CLICKED(IDC_CHECK_WHITEBALANCETEMP_AUTO, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckWhitebalancetempAuto)
	ON_CBN_SELCHANGE(IDC_COMBO_ILLUMINATION_MODE, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboIlluminationMode)
	ON_CBN_SELCHANGE(IDC_COMBO_IMAGE_RESOLUTION, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboFrametypes)

	ON_BN_CLICKED(IDC_BUTTON_FW_BROWSE, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwBrowse)
	ON_BN_CLICKED(IDC_BUTTON_FW_DOWNLOAD, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwDownload)
	ON_BN_CLICKED(IDC_BUTTON_FW_INSTALL, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwInstall)
	ON_BN_CLICKED(IDC_BUTTON_FW_CANCEL, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwCancel)

	ON_CBN_SELCHANGE(IDC_COMBO_VIDEO_MODE, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboVideoMode)
	ON_CBN_SELCHANGE(IDC_COMBO_IMAGE_FORMAT, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboImageFormat)
	ON_CBN_SELCHANGE(IDC_COMBO_POWER_USER_MODE, &CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboPowerUserMode)
	ON_BN_CLICKED(IDC_CHECK_EXPOSURE_AUTO, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckExposureAuto)
	ON_BN_CLICKED(IDC_BUTTON_WRITE_TO_FLASH, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonWriteToFlash)
	ON_BN_CLICKED(IDC_BUTTON_wb_get, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonwbget)
	ON_BN_CLICKED(IDC_BUTTON_wb_set, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonwbset)
	ON_BN_CLICKED(IDC_BUTTON_CLEAR_LOG, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedClear)
	ON_BN_CLICKED(IDC_BUTTON_LOAD_CONFIG, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonLoadConfig)
	ON_BN_CLICKED(IDC_BUTTON_RETRIEVE_CONFIG, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonRetrieveConfig)
	ON_BN_CLICKED(IDC_CHECK_DECODE_SESSION_STATUS_CHANGE_EVENT, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDecodeSessionStatusChangeEvent)
	ON_BN_CLICKED(IDC_CHECK_DETECT_BOUNDING_BOX, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDetectBoundingBox)
	ON_BN_CLICKED(IDC_BUTTON_SET_BACKGROUND, &CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSetBackground)
END_MESSAGE_MAP()


LRESULT CZebraCameraSDKSampleApplicationDlg::OnUpdateData(WPARAM a, LPARAM b)
{
	UpdateData(FALSE);
	return 0;
}

LRESULT CZebraCameraSDKSampleApplicationDlg::OnCameraAttached(WPARAM a, LPARAM b)
{
	EventLog("Camera attached.");
	UpdateCameraList();
	UpdateCameraAdapter();
	UpdateCameraDetails();
	UpdateCameraProperties();
	return 0;
}

LRESULT CZebraCameraSDKSampleApplicationDlg::OnCameraDetached(WPARAM wparam, LPARAM lparam)
{
	EventLog("Camera detached.");
	// Clear UI data
	ClearData();

	// disable controls
	EnableDisableControls(FALSE);

	UpdateCameraList();
	UpdateCameraAdapter();
	UpdateCameraDetails();
	UpdateCameraProperties();
	return 0;
}

LRESULT CZebraCameraSDKSampleApplicationDlg::UpdateDecodeSessionStatusImage(WPARAM wparam, LPARAM lparam)
{
	m_device_awake_status_bmp_.Detach();

	if (lparam == CAMERA_AWAKE)
	{
		m_device_awake_status_bmp_.Attach(m_device_awake_bmp_);
	}

	else if (lparam == CAMERA_ASLEEP)
	{
		m_device_awake_status_bmp_.Attach(m_device_sleep_bmp_);
	}

	m_device_awake_status_image_.SetBitmap(HBITMAP(m_device_awake_status_bmp_));

	return 0;
}

// Posts a message to call UpdateData function
void CZebraCameraSDKSampleApplicationDlg::UpdateDataEx()
{
	PostMessage(WM_APP + WM_CAMERA_UPDATE_NOTIFY, 0, 0);
}

// Initialize Dialog
BOOL CZebraCameraSDKSampleApplicationDlg::OnInitDialog()
{
	try
	{
		// Add "About..." menu item to system menu.
		// IDM_ABOUTBOX must be in the system command range.
		ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
		ASSERT(IDM_ABOUTBOX < 0xF000);

		// Initialize the critical section one time only.
		InitializeCriticalSectionAndSpinCount(&CriticalSection, 0x00000400);

		CMenu* pSysMenu = GetSystemMenu(FALSE);
		if (pSysMenu != nullptr)
		{
			BOOL bNameValid;
			CString strAboutMenu;
			bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
			ASSERT(bNameValid);
			if (!strAboutMenu.IsEmpty())
			{
				pSysMenu->AppendMenu(MF_SEPARATOR);
				pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
			}
		}

		// combo box controls
		m_combo_camera_list_ = (CComboBox *)GetDlgItem(IDC_COMBO_CAMERA_LIST);

		// add video modes
		m_combo_videoMode_ = (CComboBox *)GetDlgItem(IDC_COMBO_VIDEO_MODE);
		m_combo_videoMode_->AddString(STR_VIDEO_MODE_OFF);
		m_combo_videoMode_->AddString(STR_VIDEO_MODE_DEVICE_WAKEUP);
		m_combo_videoMode_->AddString(STR_VIDEO_MODE_CONTINUOUS);

		// add illumination modes
		m_combo_illuminationMode_ = (CComboBox *)GetDlgItem(IDC_COMBO_ILLUMINATION_MODE);
		m_combo_illuminationMode_->AddString(STR_ILLUMINATION_MODE_STANDARD);
		m_combo_illuminationMode_->AddString(STR_ILLUMINATION_MODE_ON);
		m_combo_illuminationMode_->AddString(STR_ILLUMINATION_MODE_OFF);

		// add orientation modes
		m_combo_imageFormat_ = (CComboBox *)GetDlgItem(IDC_COMBO_IMAGE_FORMAT);
		m_combo_imageFormat_->AddString(STR_IMAGE_FORMAT_CONVERT_TO_BMP);
		m_combo_imageFormat_->AddString(STR_IMAGE_FORMAT_CONVERT_TO_JPEG);

		// add power user mode enable/disable
		m_combo_powerUserMode_ = (CComboBox *)GetDlgItem(IDC_COMBO_POWER_USER_MODE);
		m_combo_powerUserMode_->AddString(STR_POWER_USER_MODE_DISABLE);
		m_combo_powerUserMode_->AddString(STR_POWER_USER_MODE_ENABLE);

		//add frame types;
		m_combo_frame_types_ = (CComboBox*)GetDlgItem(IDC_COMBO_IMAGE_RESOLUTION);
		
		// Set the icon for this dialog.  The framework does this automatically
		//  when the application's main window is not a dialog
		SetIcon(m_hIcon, TRUE);			// Set big icon
		SetIcon(m_hIcon, FALSE);		// Set small icon

		CDialogEx::OnInitDialog();
			 
		b_UI_initialized_ = true;


		device_manager_.AddDeviceAttachedListener(*this);
		device_manager_.AddDeviceDetachedListener(*this);

		device_info_list_ = device_manager_.EnumerateDevices();

		if (device_info_list_.size() > 0)
		{
			// Update UI
			UpdateCameraList();
			UpdateCameraAdapter();
			UpdateCameraDetails();
			UpdateCameraProperties();
		}
		else
		{
			// disable controls
			EnableDisableControls(FALSE);
		}

		m_RenderEngine_.Attach(m_picturecontrol_image_);

		// clear picture control 
		m_RenderEngine_.ClearImage();
		

		PWSTR pszPath;
		SHGetKnownFolderPath(FOLDERID_Pictures, 0, NULL, &pszPath);
		CString fullPath = pszPath;
		fullPath.AppendFormat(_T("\\%s"), _T(IMAGE_SAVE_FOLDER_DEFAULT));
		StringCbCopyW(image_save_path_, MAX_PATH, fullPath);
		m_edit_saveLocation_value_ = image_save_path_;
		CreateDirectory(image_save_path_, NULL);
		CoTaskMemFree(pszPath);

	}
	catch (const std::exception& e)
	{
		CString message(_T("OnInitDialog() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}

	return TRUE;
}

// OnSysCommand Event
void CZebraCameraSDKSampleApplicationDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// OnPaint event
void CZebraCameraSDKSampleApplicationDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// Process pre-Translation message
BOOL CZebraCameraSDKSampleApplicationDlg::PreTranslateMessage(MSG* pMsg)
{
	// ENTER key
	if ((pMsg->message == WM_KEYDOWN) &&
		(pMsg->wParam == VK_RETURN))
	{
		// Enter key was hit -> do whatever you want
		return TRUE;
	}

	return CDialog::PreTranslateMessage(pMsg);
}

// OnCtlColor Event
HBRUSH CZebraCameraSDKSampleApplicationDlg::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	CDialog::OnCtlColor(pDC, pWnd, nCtlColor);
	if (!m_brush_.m_hObject)
		m_brush_.CreateSolidBrush(RGB(247, 250, 253));
	return m_brush_;
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CZebraCameraSDKSampleApplicationDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

// Update Camera Adapter
void CZebraCameraSDKSampleApplicationDlg::UpdateCameraAdapter()
{
	try
	{
		if (device_info_list_.size() > 0)
		{
			DeviceInfo* device_info = (DeviceInfo*)m_combo_camera_list_->GetItemDataPtr(m_combo_camera_list_->GetCurSel());
			if (camera_ == nullptr)
			{
				camera_ = camera_manager_.CreateZebraCamera(*device_info);
				m_selected_camera_list_index_ = 0;
				m_selected_camera_device_path_ = device_info_list_[0].device_path;
			}

			// set selected device info object
			selected_device_info_ = device_info;

			// enable controls
			EnableDisableControls(TRUE);
			LogCameraAssetInfo(camera_);
		}
		else
		{
			camera_ = nullptr;

			// disable controls
			EnableDisableControls(FALSE);
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("UpdateCameraAdapter() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Update Camera dropdown list
void CZebraCameraSDKSampleApplicationDlg::UpdateCameraList()
{
	try
	{
		if (!b_UI_initialized_)
		{
			return;
		}

		m_combo_camera_list_->ResetContent();

		if (device_info_list_.size() > 0)
		{
			for (size_t index = 0; index < device_info_list_.size(); index++)
			{
				CString display_text;
				auto temp_cam_adapter = camera_manager_.CreateZebraCamera(device_info_list_[index]);
				try
				{
					auto sno = temp_cam_adapter->GetSerialNumber();
					display_text = CString(sno.data());
				}
				catch (const std::exception&)
				{
					Sleep(DELAY_BEFORE_ACCESS_EXTENSION_PROPERTIES);
					try
					{
						auto sno = temp_cam_adapter->GetSerialNumber();
						display_text = CString(sno.data());
					}
					catch (const std::exception& e)
					{
						CString message(_T("Camera Get Property Serial No() : Exception occurred : "));
						CString error(e.what());
						std::cout << message + error << std::endl;
						AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
					}
				}
				
				m_combo_camera_list_->SetItemDataPtr(m_combo_camera_list_->AddString(display_text), &device_info_list_[index]);
			}
			//Select the first camera available if there is any.
			if (camera_ != nullptr)
			{
				m_combo_camera_list_->SelectString(-1, CString(camera_->GetSerialNumber().c_str()));
			}
			else
			{
				m_combo_camera_list_->SetCurSel(0);
			}
		}
		else
		{
			m_combo_camera_list_->ResetContent();
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("UpdateCameraList() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// OnHScroll Event
void CZebraCameraSDKSampleApplicationDlg::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
{
	try
	{
		if (!b_UI_initialized_ || camera_ == nullptr)
		{
			return;
		}

		if (pScrollBar == (CScrollBar *)&m_slider_brightness_) {

			SliderControl<int16_t , CameraPropertyId::BRIGHTNESS> brightness_ob(&m_slider_brightness_, &m_slider_brightness_value_, &m_edit_brightness_, &m_edit_brightness_value_, &camera_->Brightness, CameraPropertyId::BRIGHTNESS , camera_->Brightness.Resolution() );
			brightness_ob.HandleSliderControl( camera_->Brightness.Value());
			m_edit_brightness_value_.Format(_T("%d"), camera_->Brightness.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_contrast_) {

			SliderControl<int16_t, CameraPropertyId::CONTRAST> contrast_ob(&m_slider_contrast_, &m_slider_contrast_value_, &m_edit_contrast_, &m_edit_contrast_value_, &camera_->Contrast, CameraPropertyId::CONTRAST , camera_->Contrast.Resolution() );
			contrast_ob.HandleSliderControl(camera_->Contrast.Value() , true);
			m_edit_contrast_value_.Format(_T("%d"), camera_->Contrast.Value());

		}
		else if (pScrollBar == (CScrollBar *)&m_slider_saturation_) {
			
			SliderControl<int16_t , CameraPropertyId::SATURATION> saturation_ob(&m_slider_saturation_, &m_slider_saturation_value_, &m_edit_saturation_, &m_edit_saturation_value_, &camera_->Saturation, CameraPropertyId::SATURATION, camera_->Saturation.Resolution());
			saturation_ob.HandleSliderControl(camera_->Saturation.Value());
			m_edit_saturation_value_.Format(_T("%d"), camera_->Saturation.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_sharpness_) {

			SliderControl<int16_t, CameraPropertyId::SHARPNESS> sharpness_ob(&m_slider_sharpness_, &m_slider_sharpness_value_, &m_edit_sharpness_, &m_edit_sharpness_value_, &camera_->Sharpness, CameraPropertyId::SHARPNESS, camera_->Sharpness.Resolution());
			sharpness_ob.HandleSliderControl(camera_->Sharpness.Value());
			m_edit_sharpness_value_.Format(_T("%d"), camera_->Sharpness.Value());

		}
		else if (pScrollBar == (CScrollBar *)&m_slider_exposure_) {

			SliderControl<uint32_t, CameraPropertyId::EXPOSURE_TIME_ABSOLUTE> exposure_ob(&m_slider_exposure_, &m_slider_exposure_value_, &m_edit_exposure_, &m_edit_exposure_value_, &camera_->AbsoluteExposureTime, CameraPropertyId::EXPOSURE_TIME_ABSOLUTE, camera_->AbsoluteExposureTime.Resolution());
			exposure_ob.HandleSliderControl(camera_->AbsoluteExposureTime.Value() , true);
			m_edit_exposure_value_.Format(_T("%d"), camera_->AbsoluteExposureTime.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_gamma_) {

			SliderControl<int16_t, CameraPropertyId::GAMMA> gamma_ob(&m_slider_gamma_, &m_slider_gamma_value_, &m_edit_gamma_, &m_edit_gamma_value_, &camera_->Gamma, CameraPropertyId::GAMMA, camera_->Gamma.Resolution());
			gamma_ob.HandleSliderControl(camera_->Gamma.Value());
			m_edit_gamma_value_.Format(_T("%d"), camera_->Gamma.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_gain_) {

			SliderControl<int16_t, CameraPropertyId::GAIN > gain_ob(&m_slider_gain_, &m_slider_gain_value_, &m_edit_gain_, &m_edit_gain_value_, &camera_->Gain, CameraPropertyId::GAIN, camera_->Gain.Resolution());
			gain_ob.HandleSliderControl(camera_->Gain.Value());
			m_edit_gain_value_.Format(_T("%d"), camera_->Gain.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_backlight_comp_) {

			SliderControl<int16_t, CameraPropertyId::BACKLIGHT_COMPENSATION > backlightcomp_ob(&m_slider_backlight_comp_, &m_slider_backlight_comp_value_ , &m_edit_backlight_comp_ , &m_edit_backlight_comp_value_ , &camera_->BacklightCompensation, CameraPropertyId::BACKLIGHT_COMPENSATION, camera_->BacklightCompensation.Resolution() );
			backlightcomp_ob.HandleSliderControl(camera_->BacklightCompensation.Value());
			m_edit_backlight_comp_value_.Format(_T("%d"), camera_->BacklightCompensation.Value());
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_wb_component_red_) {
			int blue = m_slider_wb_component_blue_.GetPos();
			int red = m_slider_wb_component_red_.GetPos();
			WhiteBalance Wb(blue , red);
			
			m_edit_wb_component_red_value_.Format(_T("%.3f"), WBFixedToDouble(red));
			m_edit_wb_red_temp_value_.Format(_T("%.3f"), WBFixedToDouble(red));

			m_slider_wb_component_red_value_ = red;
			camera_->WhiteBalanceComponent.Value(Wb);
		}
		else if (pScrollBar == (CScrollBar *)&m_slider_wb_component_blue_) {
			int blue = m_slider_wb_component_blue_.GetPos();
			int red = m_slider_wb_component_red_.GetPos();
			WhiteBalance Wb(blue, red);
			
			m_edit_whitebalance_value_.Format(_T("%.3f"), WBFixedToDouble(blue));
			m_edit_wb_blue_temp_value_.Format(_T("%.3f"), WBFixedToDouble(blue));

			m_slider_wb_component_blue_value_ = blue;
			camera_->WhiteBalanceComponent.Value(Wb);
		}
		else {
			CDialog::OnHScroll(nSBCode, nPos, pScrollBar);
		}

		UpdateDataEx();
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnHScroll() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


//Handle slider controls with a class
template <typename T, CameraPropertyId ID >
void SliderControl<T,ID>::HandleSliderControl( int prevValue) {

	int newValue = m_slider_ctrl_->GetPos();
	
	try {
		*m_slider_value_ = newValue;
		property_->Value(newValue);

	}
	catch (const std::exception&) {
		int fallbackValue = 0;
		int stepIncrement = 0;
		if (newValue > 0)
		{
			if (newValue > prevValue) {
				stepIncrement = 1;
			}
		}
		else
		{
			if (newValue < prevValue) {
				stepIncrement = -1;
			}
		}

		fallbackValue = ((newValue / step_value_) + stepIncrement) * step_value_;
		*m_slider_value_ = fallbackValue;
		m_edit_value_->Format(_T("%d"), fallbackValue);
		property_->Value(fallbackValue);
	}
}

template <typename T, CameraPropertyId ID >
void SliderControl<T, ID>::HandleSliderControl(int prevValue , bool isAuto) {

	int newValue = m_slider_ctrl_->GetPos();

	try {
		*m_slider_value_ = newValue;
		propAuto_->Value(newValue);

	}
	catch (const std::exception&) {
		int fallbackValue = 0;
		int stepIncrement = 0;
		if (newValue > 0)
		{
			if (newValue > prevValue) {
				stepIncrement = 1;
			}
		}
		else
		{
			if (newValue < prevValue) {
				stepIncrement = -1;
			}
		}

		fallbackValue = ((newValue / step_value_) + stepIncrement) * step_value_;
		*m_slider_value_ = fallbackValue;
		m_edit_value_->Format(_T("%d"), fallbackValue);
		propAuto_->Value(fallbackValue);
	}
}

// Update camera details on UI
void CZebraCameraSDKSampleApplicationDlg::UpdateCameraDetails()
{
	try
	{
		if (!b_UI_initialized_ || camera_ == nullptr)
		{
			return;
		}

		m_edit_SerialNo_value_ = camera_->GetSerialNumber().c_str();
		m_edit_modelnumber_value_ = camera_->GetModelNumber().c_str();
		m_edit_dateofmanufacture_value_ = camera_->GetDateOfManufacture().c_str();
		m_edit_dateofservice_value_ = camera_->GetFirstServiceDate().c_str();
		m_edit_dateoffirstprogram_value_ = camera_->GetDateOfFirstProgram().c_str();
		try
		{
			m_edit_firmwareversion_value_ = camera_->GetFirmwareVersion().c_str();
		}
		catch (const std::exception& e)
		{
			std::cout << e.what() << std::endl;
		}
		m_edit_hardwareversion_value_ = camera_->GetHardwareVersion().c_str();
		UpdateDataEx();
	}
	catch (const std::exception& e)
	{
		CString message(_T("UpdateCameraDetails() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

CString GetFrameTypeString(const FrameType& frame_type)
{
	auto format = frame_type.format;
	auto format_string = format == zebra::camera_sdk::FrameDataFormat::YUY2 ? "YUY2" : format == zebra::camera_sdk::FrameDataFormat::UYVY ? "UYVY" : "UNKNOWN";
	std::string frame_type_name; //= (int)frame_type.format_ + "_" /*+ (int)frame_type.width_ + "_" + (int)frame_type.height_ + "_" */ + (int)frame_type.framerate_;
	frame_type_name.append(format_string);
	frame_type_name.append("_");
	frame_type_name.append(std::to_string(frame_type.width));
	frame_type_name.append("_");
	frame_type_name.append(std::to_string(frame_type.height));
	frame_type_name.append("_");
	frame_type_name.append(std::to_string(frame_type.framerate));
	return CString(frame_type_name.c_str());
}

// Update Camera Properties
void CZebraCameraSDKSampleApplicationDlg::UpdateCameraProperties()
{
	if (!b_UI_initialized_ || camera_ == nullptr)
	{
		return;
	}

	UpdateBrightness();
	UpdateContrast();
	UpdateSaturation();
	UpdateSharpness();
	UpdateWhitebalance();
	UpdateExposure();
	UpdateGamma();
	UpdateGain();
	UpdateBacklightCompensation();

	// update frame types dropdown list
	supported_frame_types_ = camera_->GetSupportedFrameTypes();
	m_combo_frame_types_->ResetContent();

	for (int i = 0; i < (int) supported_frame_types_.size(); i++)
	{
		m_combo_frame_types_->SetItemData(m_combo_frame_types_->AddString(GetFrameTypeString(supported_frame_types_[i])), (DWORD_PTR)&supported_frame_types_[i]);
	}
	m_combo_frame_types_->SetCurSel(selected_frame_type_item);

	CheckIlluminationMode();
	CheckVideoMode();
	CheckPowerUserMode();

	m_combo_imageFormat_->SetCurSel((int)target_file_format_); // check the current image format , update image format list
	
	UpdateDataEx();
}

void CZebraCameraSDKSampleApplicationDlg::UpdateBrightness() 
{
	try
	{
		BrightnessPropValues = { camera_->Brightness.Minimum() , camera_->Brightness.Maximum() , camera_->Brightness.Value() , camera_->Brightness.Resolution() , false , false };
		UpdateSliderControl(&BrightnessPropValues, &m_slider_brightness_, &m_slider_brightness_value_, &m_edit_brightness_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Brightness"), e);
	}

}
void CZebraCameraSDKSampleApplicationDlg::UpdateContrast() 
{
	try
	{
		bool contrastAuto = camera_->Contrast.IsAutoSupported() ? true : false;
		ContrastPropValues = { camera_->Contrast.Minimum() , camera_->Contrast.Maximum() , camera_->Contrast.Value() , camera_->Contrast.Resolution() ,  camera_->Contrast.IsAutoSupported() ,  contrastAuto };
		UpdateSliderControl(&ContrastPropValues, &m_slider_contrast_, &m_slider_contrast_value_, &m_edit_contrast_value_, NULL, NULL);

	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Contrast"), e);
	}

}
void CZebraCameraSDKSampleApplicationDlg::UpdateSaturation() 
{
	try
	{
		SaturationPropValues = { camera_->Saturation.Minimum() , camera_->Saturation.Maximum() , camera_->Saturation.Value() , camera_->Saturation.Resolution() , false , false };
		UpdateSliderControl(&SaturationPropValues, &m_slider_saturation_, &m_slider_saturation_value_, &m_edit_saturation_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Saturation"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateSharpness() 
{
	try
	{
		SharpnessPropValues = { camera_->Sharpness.Minimum() , camera_->Sharpness.Maximum() , camera_->Sharpness.Value() , camera_->Sharpness.Resolution() , false , false };
		UpdateSliderControl(&SharpnessPropValues, &m_slider_sharpness_, &m_slider_sharpness_value_, &m_edit_sharpness_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Sharpness"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateWhitebalance() 
{
	try
	{
		//Update white balance component structure
		WhiteBalanceComponentBlueValue = { camera_->WhiteBalanceComponent.Minimum().Blue() , WHITE_BALANCE_COMPONENT_MAX , camera_->WhiteBalanceComponent.Value().Blue() , camera_->WhiteBalanceComponent.Resolution().Blue() ,  camera_->WhiteBalanceComponent.IsAutoSupported() ,  camera_->WhiteBalanceComponent.IsAutoEnabled() };
		WhiteBalanceComponentRedValue = { camera_->WhiteBalanceComponent.Minimum().Blue() , WHITE_BALANCE_COMPONENT_MAX , camera_->WhiteBalanceComponent.Value().Red() , camera_->WhiteBalanceComponent.Resolution().Blue() , camera_->WhiteBalanceComponent.IsAutoSupported() ,  camera_->WhiteBalanceComponent.IsAutoEnabled() };

		//update UI control
		UpdateSliderControl(&WhiteBalanceComponentRedValue, &m_slider_wb_component_red_, &m_slider_wb_component_red_value_, &m_edit_wb_component_red_value_, &m_check_white_balance_temp_auto_, &m_check_white_balance_temp_auto_value_);
		UpdateSliderControl(&WhiteBalanceComponentBlueValue, &m_slider_wb_component_blue_, &m_slider_wb_component_blue_value_, &m_edit_whitebalance_value_, &m_check_white_balance_temp_auto_, &m_check_white_balance_temp_auto_value_);

		m_edit_wb_component_red_value_.Format(_T("%.3f"), WBFixedToDouble(camera_->WhiteBalanceComponent.Value().Red()));
		m_edit_whitebalance_value_.Format(_T("%.3f"), WBFixedToDouble(camera_->WhiteBalanceComponent.Value().Blue()));

		if (camera_->WhiteBalanceComponent.IsAutoEnabled())
		{
			m_button_wbc_set_temp_.EnableWindow(FALSE);
		}
		
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("WhiteBalanceComponent"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateExposure() 
{
	try
	{
		ExposurePropValues = { (int)camera_->AbsoluteExposureTime.Minimum() , (int)camera_->AbsoluteExposureTime.Maximum() , (int)camera_->AbsoluteExposureTime.Value(), (int)camera_->AbsoluteExposureTime.Resolution() , camera_->AbsoluteExposureTime.IsAutoSupported() , camera_->AbsoluteExposureTime.IsAutoEnabled() };
		UpdateSliderControl(&ExposurePropValues, &m_slider_exposure_, &m_slider_exposure_value_, &m_edit_exposure_value_, &m_check_exposure_auto_, &m_check_exposure_auto_value_);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("AbsoluteExposureTime"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateGamma() 
{
	try
	{
		GammaPropValues = { camera_->Gamma.Minimum() , camera_->Gamma.Maximum() , camera_->Gamma.Value() , camera_->Gamma.Resolution() , false , false };
		UpdateSliderControl(&GammaPropValues, &m_slider_gamma_, &m_slider_gamma_value_, &m_edit_gamma_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Gamma"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateGain() 
{
	try
	{
		GainPropValues = { camera_->Gain.Minimum() , camera_->Gain.Maximum() , camera_->Gain.Value() , camera_->Gain.Resolution() , false , false };
		UpdateSliderControl(&GainPropValues, &m_slider_gain_, &m_slider_gain_value_, &m_edit_gain_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("Gain"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::UpdateBacklightCompensation() 
{
	try
	{
		BackLightCompPropValues = { camera_->BacklightCompensation.Minimum() , camera_->BacklightCompensation.Maximum() , camera_->BacklightCompensation.Value() , camera_->BacklightCompensation.Resolution() , false , false };
		UpdateSliderControl(&BackLightCompPropValues, &m_slider_backlight_comp_, &m_slider_backlight_comp_value_, &m_edit_backlight_comp_value_, NULL, NULL);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("BacklightCompensation"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::CheckIlluminationMode() 
{
	try
	{
		// update Illumination mode dropdown list
		auto illuminationModeValue = camera_->IlluminationModeSetting.Value();
		m_combo_illuminationMode_->SetCurSel((int)illuminationModeValue);

	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("IlluminationModeValue"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::CheckVideoMode()
{
	try
	{
		// update video mode dropdown list
		auto video_mode_Value = camera_->VideoModeSetting.Value();
		m_combo_videoMode_->SetCurSel((int)video_mode_Value);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("VideoModeValue"), e);
	}
}
void CZebraCameraSDKSampleApplicationDlg::CheckPowerUserMode()
{
	try
	{
		// update power user mode dropdown list
		auto power_user_mode_value = camera_->PowerUserModeSetting.Value();
		m_combo_powerUserMode_->SetCurSel((int)power_user_mode_value);
		(int)power_user_mode_value == 1 ? EnableCotrolOnPowerUserMode(FALSE) : EnableCotrolOnPowerUserMode(TRUE);
	}
	catch (const std::exception& e)
	{
		ShowPropertyException(_T("PowerUserMode"), e);
	}
}

// Update slider controls
void CZebraCameraSDKSampleApplicationDlg::UpdateSliderControl(UVCProperty* propertyValue, CSliderCtrl* sliderCtrl, int* sliderValue, 
	CString* sliderValueText, CButton* autoCheckCtrl, BOOL* autoValue)
{
	try
	{
		// set values to UI controls
		sliderCtrl->SetRange(propertyValue->min_value, propertyValue->max_value, TRUE);
		sliderCtrl->SetTic(propertyValue->step_value);
		sliderCtrl->SetTicFreq(propertyValue->step_value);
		sliderCtrl->SetPos(propertyValue->current_value);
		sliderValueText->Format(_T("%d"), propertyValue->current_value);
		*sliderValue = propertyValue->current_value;

		// check UI support for auto control
		if (autoCheckCtrl != NULL)
		{
			// check auto control support from the camera
			if (propertyValue->auto_control_supported)
			{
				autoCheckCtrl->EnableWindow(TRUE);

				// check auto control is enabled
				if (propertyValue->auto_control_on)
				{
					*autoValue = TRUE;
					autoCheckCtrl->SetCheck(TRUE);
					sliderCtrl->EnableWindow(FALSE);
				}
				else
				{
					*autoValue = FALSE;
					autoCheckCtrl->SetCheck(FALSE);
					sliderCtrl->EnableWindow(TRUE);
				}
			
			}
			else
			{
				autoCheckCtrl->EnableWindow(FALSE);
				autoCheckCtrl->SetCheck(FALSE);
				*autoValue = FALSE;
				sliderCtrl->EnableWindow(TRUE);
			}

		}

	}
	catch (const std::exception& e)
	{
		CString message(_T("UpdateSliderControl() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


// Set defaults button pressed event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSetdefaults()
{
	try
	{
		if (!b_UI_initialized_ || camera_ == nullptr )
		{
			return;
		}

		// call setdefaults SDK method
		camera_->SetDefaults();
		// Update UI
		UpdateCameraDetails();
		UpdateCameraProperties();
		EventLog("SetDefaults()");
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonSetdefaults() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Reboot button pressed event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonReboot()
{
	EventLog("Rebooting camera.");
	ExecuteRebootProcedures();
	EventLog("Camera rebooted.");
	// reset event registration
	ResetImageEventRegistration();


}

// Capture button clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonCapture()
{
	try
	{
		if (!b_UI_initialized_ || (camera_ == nullptr))
		{
			return;
		}

		zebra::camera_sdk::Image snapshot_image = camera_->CaptureSnapshot();
		ImageData image_data = { snapshot_image.Width(), snapshot_image.Height(), snapshot_image.Stride(), snapshot_image.Length(), snapshot_image.Data() };
		auto image_format = snapshot_image.Format();
		HandleImage(image_data, image_format, "");
		m_edit_eventType_value_ = STR_IMAGE_EVTENT_SNAPSHOT;
		EventLog("CaptureSnapshot()");

	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonCapture() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Execute reboot procedures
void CZebraCameraSDKSampleApplicationDlg::ExecuteRebootProcedures()
{
	try
	{
		if (!b_UI_initialized_ || (camera_ == nullptr))
		{
			return;
		}

		m_button_reboot_.EnableWindow(FALSE);
		camera_ = camera_manager_.Reboot(camera_);

	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonReboot() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

void CZebraCameraSDKSampleApplicationDlg::ChangeResolutionsCbEnableState()
{
	if (m_check_continuous_image_event_value_ == TRUE || m_check_decode_image_event_value_ == TRUE || m_check_snapshot_image_event_value_ == TRUE || m_check_picklist_image_value_ == TRUE)
	{
		m_combo_frame_types_->EnableWindow(FALSE);
	}
	else
	{
		m_combo_frame_types_->EnableWindow(TRUE);
	}
}

void CZebraCameraSDKSampleApplicationDlg::LogCameraAssetInfo(std::shared_ptr< ZebraCamera> camera)
{
	std::string details;
	details.append("Device Info\r\n-------------------------------------");
	details.append("\r\nserial number: ");
	details.append(camera->GetSerialNumber());
	details.append("\r\nmodel number: ");
	details.append(camera->GetModelNumber());
	details.append("\r\ndate of manufacture: ");
	details.append(camera->GetDateOfManufacture());
	details.append("\r\ndate of first program:: ");
	details.append(camera->GetDateOfFirstProgram());
	details.append("\r\ndate of first service: ");
	details.append(camera->GetFirstServiceDate());
	details.append("\r\nfirmware version: ");
	details.append(camera->GetFirmwareVersion());
	details.append("\r\nhardware version: ");
	details.append(camera->GetHardwareVersion());
	details.append("\r\n-------------------------------------");
	EventLog(details.c_str());
}

// Camera list changed event
void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboCameraList()
{
	try
	{
		if (device_info_list_.size() > 0)
		{
			CString selected_text;
			int selecetedIndex = m_combo_camera_list_->GetCurSel();
			m_combo_camera_list_->GetLBText(selecetedIndex, selected_text);
			if (selected_text != CString(camera_->GetSerialNumber().c_str()))
			{
				// reset event registration
				ResetImageEventRegistration();

				ClearData();
				auto devic_info = (DeviceInfo*)m_combo_camera_list_->GetItemDataPtr(selecetedIndex);
				camera_ = camera_manager_.CreateZebraCamera(*devic_info);
				selected_device_info_ = devic_info;
				m_selected_camera_list_index_ = selecetedIndex;

				// Update UI
				UpdateCameraDetails();
				UpdateCameraProperties();
				EventLog("CameraListChanged()");
				LogCameraAssetInfo(camera_);
			}
		}
		else
		{
			// Clear UI data
			ClearData();

			// clear camera list combo box
			m_combo_camera_list_->ResetContent();			
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboCameraList() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Clear UI data
void CZebraCameraSDKSampleApplicationDlg::ClearData()
{
	if (!b_UI_initialized_)
	{
		return;
	}

	// Camera Details
	m_edit_SerialNo_value_ = "";
	m_edit_modelnumber_value_ = "";
	m_edit_dateofmanufacture_value_ = "";
	m_edit_dateofservice_value_ = "";
	m_edit_dateoffirstprogram_value_ = "";
	m_edit_firmwareversion_value_ = "";
	m_edit_hardwareversion_value_ = "";

	// Camera Configuration
	m_edit_brightness_value_ = "";
	m_edit_contrast_value_ = "";
	m_edit_saturation_value_ = "";
	m_edit_sharpness_value_ = "";
	m_edit_whitebalance_value_ = "";
	m_edit_exposure_value_ = "";
	m_edit_gamma_value_ = "";
	m_edit_gain_value_ = "";
	m_edit_backlight_comp_value_ = "";
	m_edit_wb_component_red_value_ = "";
	m_combo_illuminationMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
	m_combo_videoMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
	m_combo_imageFormat_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
	m_combo_powerUserMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
	m_combo_frame_types_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);

	m_slider_brightness_value_ = m_slider_brightness_.GetRangeMin();
	m_slider_contrast_value_ = m_slider_contrast_.GetRangeMin();
	m_slider_saturation_value_ = m_slider_saturation_.GetRangeMin();
	m_slider_sharpness_value_ = m_slider_sharpness_.GetRangeMin();
	m_slider_wb_component_blue_value_ = m_slider_wb_component_blue_.GetRangeMin();
	m_slider_exposure_value_ = m_slider_exposure_.GetRangeMin();
	m_slider_gamma_value_ = m_slider_gamma_.GetRangeMin();
	m_slider_gain_value_ = m_slider_gain_.GetRangeMin();
	m_slider_backlight_comp_value_ = m_slider_backlight_comp_.GetRangeMin();
	m_slider_wb_component_red_value_ = m_slider_wb_component_blue_.GetRangeMin();

	m_check_white_balance_temp_auto_value_ = FALSE;
	m_check_continuous_image_event_value_ = FALSE;
	m_check_picklist_image_value_ = FALSE;
	m_check_snapshot_image_event_value_ = FALSE;
	m_check_decode_image_event_value_ = FALSE;
	m_check_decode_session_status_change_event_value_ = FALSE;
	m_check_exposure_auto_value_ = FALSE;
	// Image Event related information
	m_edit_timestamp_value_ = "";
	m_edit_eventType_value_ = "";
	m_edit_size_value_ = "";
	m_edit_format_value_ = "";
	m_edit_image_resolution_value_ = "";
	m_edit_decode_data_value_ = "";
	// Save Image related information
	m_check_autosaveimage_value_ = FALSE;
	m_button_saveButtonPressed_ = FALSE;
	m_button_browse_.EnableWindow(TRUE);
	m_button_save_.EnableWindow(TRUE);
	
	// clear picture control 
	m_RenderEngine_.ClearImage();
	UpdateDataEx();

	//clear log
	m_button_clear_log_.EnableWindow(TRUE);
}

// enable & disable UI controls
void CZebraCameraSDKSampleApplicationDlg::EnableDisableControls(BOOL enableFlag)
{
	if (!b_UI_initialized_)
	{
		return;
	}

	m_button_reboot_.EnableWindow(enableFlag);
	m_button_setdefaults_.EnableWindow(enableFlag);
	m_slider_brightness_.EnableWindow(enableFlag);
	m_slider_contrast_.EnableWindow(enableFlag);
	m_slider_saturation_.EnableWindow(enableFlag);
	m_slider_sharpness_.EnableWindow(enableFlag);
	m_slider_wb_component_blue_.EnableWindow(enableFlag);
	m_slider_exposure_.EnableWindow(enableFlag);
	m_slider_gamma_.EnableWindow(enableFlag);
	m_slider_gain_.EnableWindow(enableFlag);
	m_slider_backlight_comp_.EnableWindow(enableFlag);
	m_slider_wb_component_red_.EnableWindow(enableFlag);

	if (m_combo_camera_list_ != NULL)
	{
		m_combo_camera_list_->EnableWindow(enableFlag);
	}

	m_check_white_balance_temp_auto_.EnableWindow(enableFlag);
	m_check_exposure_auto_.EnableWindow(enableFlag);

	if (m_combo_illuminationMode_ != NULL)
	{
		m_combo_illuminationMode_->EnableWindow(enableFlag);
	}

	if (m_combo_frame_types_ != NULL)
	{
		m_combo_frame_types_->EnableWindow(enableFlag);
	}

	if (m_combo_videoMode_ != NULL)
	{
		m_combo_videoMode_->EnableWindow(enableFlag);
	}

	if (m_combo_imageFormat_ != NULL)
	{
		m_combo_imageFormat_->EnableWindow(enableFlag);
	}

	if (m_combo_powerUserMode_ != NULL)
	{
		m_combo_powerUserMode_->EnableWindow(enableFlag);
	}

	m_check_picklist_image_.EnableWindow(enableFlag);
	m_check_snapshot_image_event_.EnableWindow(enableFlag);
	m_check_decode_image_event_.EnableWindow(enableFlag);
	m_check_continuous_image_event_.EnableWindow(enableFlag);
	m_check_decode_session_status_change_event_.EnableWindow(enableFlag);

	m_edit_eventType_.EnableWindow(enableFlag);
	m_edit_size_.EnableWindow(enableFlag);
	m_edit_format_.EnableWindow(enableFlag);
	m_edit_timestamp_.EnableWindow(enableFlag);
	m_edit_imge_resolution_.EnableWindow(enableFlag);
	m_edit_decode_data_.EnableWindow(enableFlag);
	m_check_autosaveimage_.EnableWindow(enableFlag);
	m_button_browse_.EnableWindow(enableFlag);
	m_button_save_.EnableWindow(enableFlag);
	m_edit_saveLocation_.EnableWindow(enableFlag);
	m_button_clear_log_.EnableWindow(enableFlag);

	m_button_fwFileBrowse_.EnableWindow(enableFlag);
	m_button_fwDownload_.EnableWindow(enableFlag);
	m_button_fwInstall_.EnableWindow(enableFlag);
	m_button_fwCancel_.EnableWindow(enableFlag);
	m_button_capture_.EnableWindow(enableFlag);
	m_button_write_to_flash_.EnableWindow(enableFlag);
	m_button_load_config_.EnableWindow(enableFlag);
	m_button_retrieve_config_.EnableWindow(enableFlag);
	m_button_wbc_get_temp_.EnableWindow(enableFlag);
	m_button_wbc_set_temp_.EnableWindow(enableFlag);
	m_button_set_background_.EnableWindow(enableFlag);
	m_check_detect_bounding_box_.EnableWindow(enableFlag);
	UpdateDataEx();
}

// Browse button press event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonBrowse()
{
	BROWSEINFO browserInfoDialog;
	TCHAR   szDisplayName[MAX_PATH];
	ZeroMemory(&browserInfoDialog, sizeof(browserInfoDialog));
	browserInfoDialog.hwndOwner = this->m_hWnd;
	browserInfoDialog.pidlRoot = NULL;
	browserInfoDialog.pszDisplayName = szDisplayName;
	browserInfoDialog.lpszTitle = _T("Please select a folder for storing Images :");
	browserInfoDialog.ulFlags = BIF_RETURNONLYFSDIRS;
	browserInfoDialog.lParam = NULL;
	browserInfoDialog.iImage = 0;

	LPITEMIDLIST   pidl = SHBrowseForFolder(&browserInfoDialog);
	if (NULL != pidl)
	{
		BOOL bRet = SHGetPathFromIDList(pidl, image_save_path_);
		if (FALSE == bRet)
		{
			return;
		}

		// Update UI
		m_edit_saveLocation_value_ = image_save_path_;
		UpdateDataEx();
		EventLog("BrowseImages()");
	}
}

// Save Image button clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSaveImage()
{
	if (CheckSaveImageLocation())
	{
		bool check_events_enabled = FALSE;
		if (m_check_decode_image_event_value_ == TRUE || m_check_snapshot_image_event_value_ == TRUE || m_check_picklist_image_value_ == TRUE)
		{
			m_button_saveButtonPressed_ = TRUE;
			std::wstring wide_decode_data_string = m_edit_decode_data_value_.GetString();
			std::string  decode_data_string(wide_decode_data_string.begin() , wide_decode_data_string.end());
			CString file_name = GetFileName(decode_data_string);
			SaveImage(&image_buffer_, file_name);
			m_button_saveButtonPressed_ = FALSE;
			check_events_enabled = TRUE;
		}
		if (m_check_continuous_image_event_value_ == TRUE)
		{
			m_button_saveButtonPressed_ = TRUE;
			check_events_enabled = TRUE;

		}
		else if (!check_events_enabled)
		{
			AfxGetMainWnd()->MessageBox((LPCTSTR)STR_ERROR_MSG_IMAGE_EVENTS_NOT_ENABLED, (LPCTSTR)STR_ERROR);
		}
	}
}

// Validate save image path
BOOL CZebraCameraSDKSampleApplicationDlg::CheckSaveImageLocation()
{
	if (image_save_path_[0] == _T('\0'))
	{
		AfxGetMainWnd()->MessageBox(STR_ERROR_MSG_SAVE_LOCATION_NOT_SET, STR_ERROR);
		return FALSE;
	}

	DWORD dwAttrib = GetFileAttributes(image_save_path_);

	if (!(dwAttrib != INVALID_FILE_ATTRIBUTES && dwAttrib & FILE_ATTRIBUTE_DIRECTORY))
	{
		AfxGetMainWnd()->MessageBox(STR_ERROR_MSG_INVALID_SAVE_LOCATION, STR_ERROR);
		return FALSE;
	}
	return TRUE;
}

// Save Image to a file
void CZebraCameraSDKSampleApplicationDlg::SaveImage( std::vector<uint8_t>* imageBuffer, CString file_name)
{
	try
	{
		CFile saveImagefile;
		saveImagefile.Open(file_name, CFile::modeCreate | CFile::modeWrite);
		saveImagefile.SeekToBegin();
		saveImagefile.Write(imageBuffer->data(), (UINT)imageBuffer->size());
		saveImagefile.Close();
	}
	catch (...)
	{
		// check if error occurs due to save location issue
		if (!CheckSaveImageLocation())
		{
			m_check_autosaveimage_value_ = FALSE;
			m_button_save_.EnableWindow(TRUE);
			m_button_browse_.EnableWindow(TRUE);
		}
	}
}

// Auto save image checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckAutoSaveImage()
{
	if (m_check_autosaveimage_.GetCheck())
	{
		if (CheckSaveImageLocation())
		{
			m_check_autosaveimage_value_ = TRUE;
			m_button_browse_.EnableWindow(FALSE);
			m_button_save_.EnableWindow(FALSE);
		}
		else
		{
			m_check_autosaveimage_value_ = FALSE;
			m_button_save_.EnableWindow(TRUE);
			m_button_browse_.EnableWindow(TRUE);
		}
	}
	else
	{
		m_check_autosaveimage_value_ = FALSE;
		m_button_save_.EnableWindow(TRUE);
		m_button_browse_.EnableWindow(TRUE);
	}

	UpdateDataEx();
}


// Continuous Image Event checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckContinuousImageEvent()
{
	if (camera_ == NULL)
	{
		m_check_continuous_image_event_value_ = FALSE;
		return;
	}

	if (m_check_continuous_image_event_.GetCheck())
	{
		m_check_continuous_image_event_value_ = TRUE;

		EventLog("ContinuousImage()");
		// register for continuous image event
		camera_->AddContinuousImageEventListener(*continuous_image_event_handler_);
	}
	else
	{
		camera_->RemoveContinuousImageEventListener(*continuous_image_event_handler_);
		
		m_check_continuous_image_event_value_ = FALSE;
	}
	ChangeResolutionsCbEnableState();
	// update UI
	UpdateDataEx();
}


// Snapshot Image Event checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckSnapshotImageEvent()
{
	if (camera_ == NULL)
	{
		m_check_snapshot_image_event_value_ = FALSE;
		return;
	}

	if (m_check_snapshot_image_event_.GetCheck())
	{
		m_check_snapshot_image_event_value_ = TRUE;

		EventLog("SnapshotImage()");
		// register for snapshot image event
		camera_->AddSnapshotImageEventListener(*snapshot_image_event_handler_);
	}
	else
	{
		camera_->RemoveSnapshotImageEventListener(*snapshot_image_event_handler_);
	
		m_check_snapshot_image_event_value_ = FALSE;
	}
	ChangeResolutionsCbEnableState();

	// update UI
	UpdateDataEx();
}

// Decode Image Event checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDecodeImageEvent()
{
	if (camera_ == NULL)
	{
		m_check_decode_image_event_value_ = FALSE;
		return;
	}

	if (m_check_decode_image_event_.GetCheck())
	{
		m_check_decode_image_event_value_ = TRUE;

		EventLog("DecodeImage()");
		// register for decode image event
		camera_->AddDecodeImageEventListener(*decode_image_event_handler_);;
	}
	else
	{
		camera_->RemoveDecodeImageEventListener(*decode_image_event_handler_);
		
		m_check_decode_image_event_value_ = FALSE;
	}
	ChangeResolutionsCbEnableState();
	// update UI
	UpdateDataEx();
}

// Produce Image Event checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckProduceImageEvent()
{
	if (camera_ == NULL)
	{
		m_check_picklist_image_value_ = FALSE;
		return;
	}

	if (m_check_picklist_image_.GetCheck())
	{
		m_check_picklist_image_value_ = TRUE;

		EventLog("ProduceImage()");
		// register for produce image event
		camera_->AddProduceImageEventListener(*produce_image_event_handler_);
	}
	else
	{
		camera_->RemoveProduceImageEventListener(*produce_image_event_handler_);
		m_check_picklist_image_value_ = FALSE;
	}
	ChangeResolutionsCbEnableState();

	// update UI
	UpdateDataEx();
}

// Decode Session Status Event checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDecodeSessionStatusChangeEvent()
{
	if (camera_ == NULL)
	{
		m_check_decode_session_status_change_event_value_ = FALSE;
		return;
	}

	if (m_check_decode_session_status_change_event_.GetCheck())
	{
		m_check_decode_session_status_change_event_value_ = TRUE;

		EventLog("DecodeSessionStatusChangeEvent()");
		// register for decode session status event
		camera_->AddDecodeSessionStatusChangeEventListener(*decode_session_status_change_event_handler_);

		SetDeviceAwakeStatusImage(DecodeSessionStatus::DECODE_SESSION_END);

	}
	else
	{
		camera_->RemoveDecodeSessionStatusChangeEventListener(*decode_session_status_change_event_handler_);
		m_check_decode_session_status_change_event_value_ = FALSE;

		m_device_awake_status_image_.SetBitmap(NULL);
	}
	ChangeResolutionsCbEnableState();

	// update UI
	UpdateDataEx();
}

 // Snapshot Image received Event
void SnapshotImageEventHandler::ImageReceived(ImageEventData ev, ImageEventMetaData evm)
{
	m_pDlg_->m_edit_eventType_value_ = STR_IMAGE_EVTENT_SNAPSHOT;
	m_pDlg_->HandleImage(ev.image, ev.format);
}

// Produce Image received Event
void ProduceImageEventHandler::ImageReceived(ImageEventData ev, ImageEventMetaData evm)
{
	m_pDlg_->m_edit_eventType_value_ = STR_IMAGE_EVTENT_PRODUCE;
	m_pDlg_->HandleImage(ev.image, ev.format);
}


// Decode Image received Event
void DecodeImageEventHandler::ImageReceived(ImageEventData ev, ImageEventMetaData evm)
{
	m_pDlg_->m_edit_eventType_value_ = STR_IMAGE_EVTENT_DECODE;
	m_pDlg_->HandleImage(ev.image, ev.format ,evm.decode_data);
}


// Continuous Image received Event
void ContinuousImageEventHandler::ImageReceived(ImageEventData ev, ImageEventMetaData evm)
{
	m_pDlg_->m_edit_eventType_value_ = STR_IMAGE_EVTENT_CONTINUOUS;
	m_pDlg_->HandleImage(ev.image, ev.format);
}

// Decode session Status Changed Event
void DecodeSessionStatusChangeEventHandler::DecodeSessionStatusChanged(DecodeSessionStatus status)
{
	m_pDlg_->SetDeviceAwakeStatusImage(status);
}


 // Device Attached
void CZebraCameraSDKSampleApplicationDlg::Attached(DeviceInfo info)
{
	try
	{

		if (device_info_list_.size() == 0)
		{
			device_info_list_.push_back(info);
		}
		else
		{
			std::string checkPath = info.device_path;
			bool found = false;
			for (size_t i = 0; i < device_info_list_.size(); i++)
			{
				if (device_info_list_[i].device_path == checkPath)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				device_info_list_.push_back(info);
			}
		}

		//Update UI
		if (b_UI_initialized_)
		{
			PostMessage(WM_APP + WM_CAMERA_ATTACHED_NOTIFY, 0, 0);
		}

	}
	catch (const std::exception& e)
	{
		CString message(_T("onDeviceAdded() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

 // Device Detached
void CZebraCameraSDKSampleApplicationDlg::Detached(DeviceInfo info)
{
	try
	{
		if (device_info_list_.size() != 0)
		{
			std::string checkPath = info.device_path;
			size_t foundIndex = INVALID_INDEX;

			if (selected_device_info_->device_path == info.device_path)
			{
				// reset event registration
				ResetImageEventRegistration();

				camera_ = nullptr;
			}

			auto it = std::remove_if(device_info_list_.begin(), device_info_list_.end(),
				[info](DeviceInfo element)->bool
				{
					return info.device_path == element.device_path;
				});
			device_info_list_.erase(it,device_info_list_.end());

			//Update UI
			if (b_UI_initialized_)
			{
				PostMessage(WM_APP + WM_CAMERA_DETACHED_NOTIFY, 0, 0);
			}

		}
		else
		{
			m_combo_camera_list_->ResetContent();
			m_combo_illuminationMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
			m_combo_frame_types_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
			m_combo_videoMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
			m_combo_imageFormat_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);
			m_combo_powerUserMode_->SetCurSel(COMBO_ITEM_NOT_SELECTED_VALUE);

			//Update UI
			if (b_UI_initialized_)
			{
				PostMessage(WM_APP + WM_CAMERA_DETACHED_NOTIFY, 0, 0);
			}
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("onDeviceRemoved() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Firmware download event
void FirmwareDownloadEventHandler::EventReceived(FirmwareDownloadEventsArgs evargs)
{

	switch (evargs.GetOperationCode())
	{
	case FirmwareDownloadEventsArgs::OperationCode::kSessionStart:
		m_pDlg_->m_progressbar_fwDownload_.SetRange32(0, evargs.GetTotalRecords());
		m_pDlg_->EventLog("FirmwareDownload():Session Start()");
		break;
	case FirmwareDownloadEventsArgs::OperationCode::kProgress:
		m_pDlg_->m_progressbar_fwDownload_.SetPos(evargs.GetCurrentRecord());
		break;
	case FirmwareDownloadEventsArgs::OperationCode::kDownloadEnd:
		m_pDlg_->EventLog("FirmwareDownload():Download End()");
		break;
	case FirmwareDownloadEventsArgs::OperationCode::kSessionStop:
		m_pDlg_->m_progressbar_fwDownload_.SetPos(0);
		m_pDlg_->EventLog("FirmwareDownload():Session Stop()");
		break;
	case FirmwareDownloadEventsArgs::OperationCode::kError:
		m_pDlg_->m_progressbar_fwDownload_.SetPos(0);
		m_pDlg_->camera_manager_.RemoveFirmwareDownloadEventListener(*this);
		CString message(_T("Firmware download failed. "));
		CString error;
		error.Format(_T("Error = %d"), evargs.GetStatus());
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
		break;
	}
}

zebra::image::FileConverter CZebraCameraSDKSampleApplicationDlg::GetFileConverter(ImageFormat& format)
{
	switch (format)
	{
	case ImageFormat::YUY2:
		return target_file_format_ == TargetFileFormat::BMP ? zebra::image::FileConverter::YUY2_TO_BMP : zebra::image::FileConverter::YUY2_TO_JPEG;
	case ImageFormat::UYVY:
		return target_file_format_ == TargetFileFormat::BMP ? zebra::image::FileConverter::UYVY_TO_BMP : zebra::image::FileConverter::UYVY_TO_JPEG;
	default:
		return target_file_format_ == TargetFileFormat::BMP ? zebra::image::FileConverter::YUY2_TO_BMP : zebra::image::FileConverter::YUY2_TO_JPEG;
		break;
	}
}

// Update the UI by showing the image received
void CZebraCameraSDKSampleApplicationDlg::ShowImage(std::vector<uint8_t>& image ,std::string decode_data , uint32_t width , uint32_t height , size_t size)
{
	try
	{
		// draw image on view
		m_RenderEngine_.Render((LPBYTE)image.data(), (DWORD)image.size());

		// update image related dataYYYY-MM-DD hh:mm:ss
		SYSTEMTIME system_time;
		GetSystemTime(&system_time);
		auto milliseconds = std::to_string(system_time.wMilliseconds);
		CString ms;
		ms.Format(L":%d", system_time.wMilliseconds);
		CString current_time = CTime::GetCurrentTime().Format("%Y-%m-%d %H:%M:%S");
		current_time.Append(ms);
		m_edit_format_value_ = target_file_format_ == TargetFileFormat::BMP ? STR_IMAGE_FORMAT_BMP : target_file_format_ == TargetFileFormat::JPEG ? STR_IMAGE_FORMAT_JPEG : _T(""); // todo : use type param to set this
		m_edit_size_value_.Format(_T("%.*f"), IMAGE_SIZE_PRECISION, (image.size() / IMAGE_SIZE_KB_DEVISOR));
		m_edit_timestamp_value_ = current_time;
		m_edit_image_resolution_value_.Format(_T("%d x %d"), width, height);
		CString cs(decode_data.c_str());
		m_edit_decode_data_value_.Format(_T("%s"), cs);

		if (m_check_decode_image_event_value_ == TRUE || m_check_snapshot_image_event_value_ == TRUE || m_check_picklist_image_value_ == TRUE)
		{
			image_buffer_ = image; //save the image buffer if it is produce , decode or snapshot event data

		}

		// update UI
		UpdateDataEx();
	}
	catch (const std::exception& e)
	{
		CString message(_T("ShowImage() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

//Generate File Name
CString CZebraCameraSDKSampleApplicationDlg::GetFileName(std::string decode_data_str)
{
	CString currentTime = CTime::GetCurrentTime().Format("%Y%m%d_%H%M%S");
	SYSTEMTIME systemTime;
	GetSystemTime(&systemTime);
	auto seconds = std::to_string(systemTime.wMilliseconds);
	CString secondsStr(seconds.c_str());
	currentTime = currentTime + L"_" + secondsStr + L"_";

	
	std::string illegal_chars("\\/:?\"<>|");  // Remove illegal characters for file names , from the string
	std::string::iterator it;

	for(it= decode_data_str.begin(); it!= decode_data_str.end();++it)
	{
		BOOL found = illegal_chars.find(*it) != std::string::npos;
		if (found) {
			*it=' ';
		}
	}

	decode_data_str = decode_data_str + "_";

	CString image_source("c");
	// decide file extension to save image
	CString fileExtention;
	if (target_file_format_ == TargetFileFormat::BMP)
	{
		fileExtention = IMAGE_EXT_BMP;
	}
	else if (target_file_format_ == TargetFileFormat::JPEG)
	{
		fileExtention = IMAGE_EXT_JPEG;
	}
	else
	{
		fileExtention = "";
	}

	CString csFoldereName(image_save_path_);
	CString csFileName = csFoldereName + L"\\" + IMAGE_NAME_PREFIX + currentTime + decode_data_str.c_str() + image_source + fileExtention;

	return csFileName;
}

// Update the device awake status 
void CZebraCameraSDKSampleApplicationDlg::SetDeviceAwakeStatusImage(DecodeSessionStatus status)
{

	if (status == DecodeSessionStatus::DECODE_SESSION_START)
	{
		PostMessage(WM_APP + WM_CAMERA_DECODE_SESSION_STATUS_NOTIFY, 0, CAMERA_AWAKE);
	}
	else if (status == DecodeSessionStatus::DECODE_SESSION_END)
	{
		PostMessage(WM_APP + WM_CAMERA_DECODE_SESSION_STATUS_NOTIFY, 0, CAMERA_ASLEEP);
	}
}

//Handle image-show and save
void CZebraCameraSDKSampleApplicationDlg::HandleImage(ImageData& evdat, ImageFormat& format, std::string decode_data) 
{
	converter_type_ = GetFileConverter(format);
	std::vector<uint8_t> image = zebra::image::Encode(converter_type_, evdat);
	std::string temp_decode_data = "";
	auto it = decode_data.begin();
	for (; it < decode_data.end(); it++)
	{
		if (*it < 0x20)
		{
			temp_decode_data.append(" ");
		}
		else
		{
			auto s = *it;
			temp_decode_data.append(1,*it);
		}
	}
	ShowImage(image, temp_decode_data , evdat.width , evdat.height , evdat.size);
	if (m_check_autosaveimage_value_ || m_button_saveButtonPressed_)
	{
		CString csFileName = GetFileName(temp_decode_data);
		std::future<void> result(std::async(std::launch::async | std::launch::deferred, &CZebraCameraSDKSampleApplicationDlg::SaveImage, this, &image, csFileName));
		result.get();
		m_button_saveButtonPressed_ = FALSE;
	}
}

// OnClose Event
void CZebraCameraSDKSampleApplicationDlg::OnClose()
{
	// reset event registration
	ResetImageEventRegistration();

	// Release resources used by the critical section object.
	DeleteCriticalSection(&CriticalSection);

	CDialogEx::OnClose();
}

// reset event registration
void CZebraCameraSDKSampleApplicationDlg::ResetImageEventRegistration()
{
	if (camera_ != nullptr) {
		if (continuous_image_event_handler_ != nullptr)
			camera_->RemoveContinuousImageEventListener(*continuous_image_event_handler_);
		if (produce_image_event_handler_ != nullptr)
			camera_->RemoveProduceImageEventListener(*produce_image_event_handler_);
		if (snapshot_image_event_handler_ != nullptr)
			camera_->RemoveSnapshotImageEventListener(*snapshot_image_event_handler_);
		if (decode_image_event_handler_ != nullptr)
			camera_->RemoveDecodeImageEventListener(*decode_image_event_handler_);
		if (decode_session_status_change_event_handler_ != nullptr)
			camera_->RemoveDecodeSessionStatusChangeEventListener(*decode_session_status_change_event_handler_);
	}
	if (firmware_download_event_handler_ != nullptr)
		camera_manager_.RemoveFirmwareDownloadEventListener(*firmware_download_event_handler_);
}


// White Balance Temperature Auto checkbox clicked event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckWhitebalancetempAuto()
{
	if (camera_ == NULL)
	{
		m_check_white_balance_temp_auto_value_ = FALSE;
		return;
	}

	try
	{
		if (m_check_white_balance_temp_auto_.GetCheck())
		{
			m_check_white_balance_temp_auto_value_ = TRUE;

			// Set white balance temp auto control on
			camera_->WhiteBalanceComponent.AutoEnable(true);

			// disable slider control
			m_slider_wb_component_blue_.EnableWindow(FALSE);
			m_slider_wb_component_red_.EnableWindow(FALSE);

			// enable get/set WB values
			m_edit_wb_blue_temp_.SetReadOnly(TRUE);
			m_edit_wb_red_temp_.SetReadOnly(TRUE);
			m_button_wbc_set_temp_.EnableWindow(FALSE);
		}
		else
		{
			m_check_white_balance_temp_auto_value_ = FALSE;

			// Set white balance temp auto control on
			camera_->WhiteBalanceComponent.AutoEnable(false);

			// enable slider control
			m_slider_wb_component_blue_.EnableWindow(TRUE);
			m_slider_wb_component_red_.EnableWindow(TRUE);

			// enable get/set WB values
			m_edit_wb_blue_temp_.SetReadOnly(FALSE);
			m_edit_wb_red_temp_.SetReadOnly(FALSE);
			m_button_wbc_set_temp_.EnableWindow(TRUE);
		}

		// update UI
		UpdateDataEx();
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedCheckWhitebalancetempAuto() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Exposure auto check box change event
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckExposureAuto()
{
	if (camera_ == NULL)
	{
		m_check_exposure_auto_value_ = FALSE;
		return;
	}

	try
	{
		if (m_check_exposure_auto_.GetCheck())
		{
			m_check_exposure_auto_value_ = TRUE;

			// Set Exposure auto control on
			camera_->AbsoluteExposureTime.AutoEnable(true);

			// disable slider control
			m_slider_exposure_.EnableWindow(FALSE);
		}
		else
		{
			m_check_exposure_auto_value_ = FALSE;

			// Set Exposure auto control off
			camera_->AbsoluteExposureTime.AutoEnable(false);;

			// enable slider control
			m_slider_exposure_.EnableWindow(TRUE);
		}

		// update UI
		UpdateDataEx();
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedCheckExposureAuto() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}

}

// Illumination mode changed event
void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboIlluminationMode()
{
	if (camera_ == NULL || device_info_list_.size() == 0)
	{
		m_combo_illuminationMode_->EnableWindow(FALSE);
		return;
	}

	try
	{
		// set illumination mode
		auto selected_item = m_combo_illuminationMode_->GetCurSel();
		camera_->IlluminationModeSetting.Value((IlluminationMode)selected_item);
		UpdateCameraProperties();
		EventLog("IlluminationModeChanged():" , selected_item);
		
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboIlluminationMode() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboFrametypes()
{
	if (camera_ == NULL || device_info_list_.size() == 0)
	{
		m_combo_frame_types_->EnableWindow(FALSE);
		return;
	}

	try
	{
		selected_frame_type_item = m_combo_frame_types_->GetCurSel();
		FrameType* frame_type = (FrameType*)m_combo_frame_types_->GetItemData(selected_frame_type_item);
		camera_->SetCurrentFrameType(*frame_type);
		EventLog("OnCbnSelchangeComboFrametypes():", selected_frame_type_item);
		m_RenderEngine_.ClearImage();
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboFrametypes() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


// Firmware dat file browse button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwBrowse()
{
	const TCHAR szFilter[] = _T("Dat Files (*.dat)|*.dat|");
	CFileDialog fwFileDlg(TRUE, _T("dat"), NULL, OFN_HIDEREADONLY | OFN_FILEMUSTEXIST, szFilter, this);
	fwFileDlg.m_ofn.lpstrTitle = STR_DIALOG_TITLE_SELECT_FIRMWARE_FILE;
	if (fwFileDlg.DoModal() == IDOK)
	{
		CString sFilePath = fwFileDlg.GetPathName();

		// Update UI
		m_edit_fwFilePath_value_ = sFilePath;
		UpdateDataEx();
	}
}

// Download firmware button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwDownload()
{
	if (!PathFileExists(m_edit_fwFilePath_value_))
	{
		AfxGetMainWnd()->MessageBox(STR_ERROR_MSG_INVALID_FIRMWARE_FILE, (LPCTSTR)STR_ERROR);
		return;
	}

	// call download firmware
	try
	{
		if (camera_ != nullptr) {
			std::string firmware_file_path(CW2A(m_edit_fwFilePath_value_.GetString()));
			camera_manager_.AddFirmwareDownloadEventListener(*firmware_download_event_handler_);
			camera_manager_.DownloadFirmware(camera_,firmware_file_path);
			EventLog("Download Firmware");
		}
	}
	catch (const std::exception& e)
	{
		camera_manager_.RemoveFirmwareDownloadEventListener(*firmware_download_event_handler_);
		CString message(_T("OnBnClickedButtonFwDownload() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}

}


// Install firmware button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwInstall()
{
	try
	{
		if (camera_ != nullptr) {
			camera_ = camera_manager_.InstallFirmware(camera_ , 10000);
			EventLog("InstallFirmware()");
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonFwInstall() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


// Cancel firmware download button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonFwCancel()
{
	try
	{
		if (camera_ != nullptr) {
			camera_manager_.CancelFirmwareDownload(camera_);
			EventLog("CancelFirmwareDownload()");
		}
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonFwCancel() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Read custom Product ID from config file
int ReadCustomPID()
{
	int pid = PRODUCT_ID;
	try
	{
		FILE* file;
		char buffer[100];
		if (0 == fopen_s(&file, "config.txt", "r"))
		{
			if (file)
			{
				fread(buffer, 5, 1, file);
				auto temp_pid = atoi(buffer);
				if (temp_pid != 0)
				{
					pid = temp_pid;
				}
				fclose(file);
			}
		}
	}
	catch (...)
	{
		;
	}
	return pid;
}

// Video mode changed event
void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboVideoMode()
{
	if (camera_ == NULL || device_info_list_.size() == 0)
	{
		m_combo_videoMode_->EnableWindow(FALSE);
		return;
	}

	try
	{
		// set video mode
		auto selected_item = m_combo_videoMode_->GetCurSel();

		switch (selected_item) {
			case 0:
				camera_->VideoModeSetting.Value(VideoMode::OFF);
				break;
			case 1:
				camera_->VideoModeSetting.Value(VideoMode::WAKEUP);
				break;
			case 2:
				camera_->VideoModeSetting.Value(VideoMode::CONTINUOUS);

		}	

		UpdateCameraProperties();
		EventLog("VideoModeChanged():", selected_item);
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboVideoMode() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


// Image format changed event
void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboImageFormat()
{
	if (camera_ == NULL || device_info_list_.size() == 0)
	{
		m_combo_imageFormat_->EnableWindow(FALSE);
		return;
	}

	try
	{
		// set image format
		auto selected_item = m_combo_imageFormat_->GetCurSel();
		target_file_format_ = selected_item == 1 ? TargetFileFormat::JPEG : TargetFileFormat::BMP;
		UpdateCameraProperties();
		EventLog("ImageFormatChanged():", selected_item);
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboImageFormat() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

// Power User Mode changed event
void CZebraCameraSDKSampleApplicationDlg::OnCbnSelchangeComboPowerUserMode()
{
	if (camera_ == NULL || device_info_list_.size() == 0)
	{
		m_combo_powerUserMode_->EnableWindow(FALSE);
		return;
	}

	try
	{
		auto selected_item = m_combo_powerUserMode_->GetCurSel();
		
		if (selected_item == 1)
		{
			// display confirmation dialog
			int msgboxID = MessageBox(
				(LPCWSTR)L"WARNING !!! The Power User Mode will be activated. Some UVC properties will be deactivated.\n\nDo you want to continue?   ",
				(LPCWSTR)L"Confirm Action",
				(MB_ICONWARNING | MB_YESNO | MB_DEFBUTTON2));

			if (msgboxID == IDYES)
			{
				EnableCotrolOnPowerUserMode(FALSE);

				camera_->PowerUserModeSetting.Value(PowerUserMode::ENABLE);
			}
		}
		else 
		{
			EnableCotrolOnPowerUserMode(TRUE);
			camera_->PowerUserModeSetting.Value(PowerUserMode::DISABLE);
		}

		UpdateCameraProperties();
		EventLog("PowerUserModeChanged():", selected_item);
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnCbnSelchangeComboPowerUserMode() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}

void CZebraCameraSDKSampleApplicationDlg::EnableCotrolOnPowerUserMode(bool flag)
{
	if (!m_check_white_balance_temp_auto_.GetCheck())
	{
		m_slider_wb_component_blue_.EnableWindow(flag);
		m_slider_wb_component_red_.EnableWindow(flag);
	}
	m_check_white_balance_temp_auto_.EnableWindow(flag);
	if (!m_check_exposure_auto_.GetCheck())
	{
		m_slider_exposure_.EnableWindow(flag);
	}
	m_check_exposure_auto_.EnableWindow(flag);
	m_slider_gain_.EnableWindow(flag);
}

// Write to flash button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonWriteToFlash()
{
	try
	{
		if (!b_UI_initialized_ || camera_ == nullptr )
		{
			return;
		}
		// perform write to flash
		camera_->WriteToFlash();
		EventLog("WriteToFlash()");
	}
	catch (const std::exception& e)
	{
		CString message(_T("OnBnClickedButtonWriteToFlash() : Exception occurred : "));
		CString error(e.what());
		std::cout << message + error << std::endl;
		AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	}
}


void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonwbget()
{
	int blue = 0, red = 0;
	blue = camera_->WhiteBalanceComponent.Value().Blue(); 
	red = camera_->WhiteBalanceComponent.Value().Red(); 

	m_edit_wb_blue_temp_value_.Format(_T("%.3f"), WBFixedToDouble(blue));
	m_edit_wb_red_temp_value_.Format(_T("%.3f"), WBFixedToDouble(red));

	UpdateDataEx();
}


void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonwbset()
{
	double blue, red;
	uint16_t shourt_blue, short_red;

	m_edit_wb_red_temp_.GetWindowText(m_edit_wb_red_temp_value_);
	m_edit_wb_blue_temp_.GetWindowText(m_edit_wb_blue_temp_value_);

	wchar_t* stop_char;
	blue = wcstod(m_edit_wb_blue_temp_value_, &stop_char);
	red = wcstod(m_edit_wb_red_temp_value_, &stop_char);

	m_edit_whitebalance_value_.Format(_T("%.3f"), blue);
	m_edit_wb_component_red_value_.Format(_T("%.3f"), red);

	shourt_blue = WBDoubleToFixed(blue);
	short_red = WBDoubleToFixed(red);

	WhiteBalance Wb((int)shourt_blue, (int)short_red);
	camera_->WhiteBalanceComponent.Value(Wb);
	
	m_slider_wb_component_blue_value_ = camera_->WhiteBalanceComponent.Value().Blue();
	m_slider_wb_component_red_value_ = camera_->WhiteBalanceComponent.Value().Red();

	UpdateDataEx();
}

// Clear the event log button click
void CZebraCameraSDKSampleApplicationDlg::OnBnClickedClear()
{
	m_edit_log_.GetWindowText(m_edit_log_value_);
	m_edit_log_value_ = " ";
	m_edit_eventType_value_ = "";
	m_edit_size_value_ = "";
	m_edit_format_value_ = "";
	m_edit_timestamp_value_ = "";
	m_edit_decode_data_value_ = "";
	m_edit_image_resolution_value_ = "";
	m_RenderEngine_.ClearImage();
	UpdateDataEx();

}


void CZebraCameraSDKSampleApplicationDlg::EventLog( const char * event_description)
{
	CString message;
	message.Format(L"\r\n%s", (CString)event_description);
	m_edit_log_.GetWindowText(m_edit_log_value_);
	m_edit_log_value_ = m_edit_log_value_ + message;
	m_edit_log_.SetWindowText(m_edit_log_value_);
	UpdateDataEx();
	
}

void CZebraCameraSDKSampleApplicationDlg::EventLog(const char * event_description , int value)
{
	CString message;
	message.Format(L"\r\n%s%d" , (CString)event_description, value);
	m_edit_log_.GetWindowText(m_edit_log_value_);
	m_edit_log_value_ = m_edit_log_value_ + message;
	m_edit_log_.SetWindowText(m_edit_log_value_);
	UpdateDataEx();
}


double CZebraCameraSDKSampleApplicationDlg::WBFixedToDouble(uint16_t  val)
{
	return ((double)val / (double)(1 << FLOATING_POINT));
}

uint16_t  CZebraCameraSDKSampleApplicationDlg::WBDoubleToFixed(double val)
{
	return (uint16_t)(val * (1 << FLOATING_POINT));
}

void CZebraCameraSDKSampleApplicationDlg::ShowPropertyException(CString property_name , const std::exception& e)
{
	CString message(_T("Updatecamera_Properties(): ") + property_name + _T(" : Exception occurred : "));
	CString error(e.what());
	std::cout << message + error << std::endl;
	AfxGetMainWnd()->MessageBox((LPCTSTR)(message + error), (LPCTSTR)STR_ERROR);
	CStringA exception_text((property_name + _T(" : Exception occurred : ")));
	EventLog((const char*)exception_text);
	EventLog(e.what());

}

void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonLoadConfig()
{
	EventLog("Loading configuration");
	const TCHAR file_name_filter[] = _T("bcccfg Files (*.bcccfg)|*.bcccfg|");
	CFileDialog load_config_file_dlg(TRUE, _T("bcccfg"), NULL, OFN_HIDEREADONLY | OFN_FILEMUSTEXIST, file_name_filter, this);
	load_config_file_dlg.m_ofn.lpstrTitle = _T(STR_DIALOG_TITLE_LOAD_CONFIGURATION);
	if (load_config_file_dlg.DoModal() == IDOK)
	{
		CString file_path = load_config_file_dlg.GetPathName();
		std::string camcfg_file_path(CW2A(file_path.GetString()));
		EventLog(camcfg_file_path.c_str());
		try
		{
			camera_manager_.LoadConfigurationFromFile(camera_, camcfg_file_path);
			EventLog("Succeeded");
			MessageBox(_T("Load configuration succeeded."), _T("Load configuration"));
		}
		catch (const std::exception& e)
		{
			CString message;
			message = "Load configuration failed. Error : ";
			message.Append(CString(e.what()));
			EventLog("Failed");
			MessageBox(message, _T("Load configuration"));
		}
		UpdateCameraProperties();
		UpdateDataEx();
	}
}


void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonRetrieveConfig()
{
	EventLog("Retrieve configuration");
	try
	{
		CString current_time = CTime::GetCurrentTime().Format("_%Y.%m.%d");
		CString file_name("Config_file_");
		file_name.Append(m_edit_modelnumber_value_);
		file_name.Append(current_time);
		file_name.Append(CString(".bcccfg"));

		auto configuration = camera_manager_.RetrieveConfiguration(camera_);

		char file_name_filter[] = { "bcccfg Files (*.bcccfg)|*.bcccfg|" };
		CFileDialog save_config_dlg(FALSE, CString(".bcccfg"), file_name, OFN_OVERWRITEPROMPT, CString(file_name_filter));
		save_config_dlg.m_ofn.lpstrTitle = _T(STR_DIALOG_TITLE_RETIEVE_CONFIGURATION);

		if (save_config_dlg.DoModal() == IDOK)
		{
			auto save_path = save_config_dlg.GetPathName();
			std::ofstream out(save_path);
			out << configuration;
			out.close();
			EventLog(CW2A(save_path.GetString()));
			MessageBox(_T("Retrieve and save configuration succeeded."), _T("Retrieve configuration"));
		}
	}
	catch (const std::exception& e)
	{
		CString message;
		message = "Retrieve configuration failed. Error : ";
		message.Append(CString(e.what()));
		EventLog("Failed");
		MessageBox(message, _T("Retrieve configuration"));
	}
}


void CZebraCameraSDKSampleApplicationDlg::OnBnClickedCheckDetectBoundingBox()
{
	UpdateData(TRUE);
	m_RenderEngine_.DetectBoundingBox(m_detect_bounding_box_);
}


void CZebraCameraSDKSampleApplicationDlg::OnBnClickedButtonSetBackground()
{
	UpdateData(TRUE);
	m_RenderEngine_.DetectBoundingBox(false);
	m_RenderEngine_.SaveBackground();
	OnBnClickedButtonCapture();
	m_RenderEngine_.DetectBoundingBox(m_detect_bounding_box_);
}
