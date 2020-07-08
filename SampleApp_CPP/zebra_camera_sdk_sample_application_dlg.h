
// ZebraCameraSDKSampleApplicationDlg.h : header file
//

#pragma once

#include "afxwin.h"

#include <GdiPlus.h>
using namespace Gdiplus;


#include <iostream>
#include <vector>
#include <windows.h>
#include "device_manager.h"
#include "image_converter.h"
#include "zebra_camera.h"
#include "zebra_camera_manager.h"

#include "camera_bounding_box_factory.h"

#pragma comment(lib, "device_manager.lib")
#pragma comment(lib, "zebracam.lib")
#pragma comment(lib, "image_converter.lib")

using namespace zebra;
using namespace zebra::camera_sdk;
using namespace zebra::image;
using namespace std;

//Target file format
enum TargetFileFormat
{
	BMP,
	JPEG
};

// UVC property structure
struct UVCProperty {
	int min_value;
	int max_value;
	int current_value;
	int step_value;
	bool auto_control_supported;
	bool auto_control_on;
};

// Image information
struct ImageInfo
{
	int width;
	int height;
	CString image_format;
	CString attrib_info;
	bool save_frames;
	bool convert_to_BMP;
	unsigned long image_size = 0;
	unsigned long padding = 0;
	BYTE *image_data;
};


/***
Class:		 CRender
Description: Image and Video Rendering helper class based on GDI+
***/
class CRender
{
	static const long GLOBAL_MEM = 2 * 1024 * 1024; //Global memory = 2MB
	CameraBoundingBoxDetectorInterface* frameProc;
	vector<BoundingBoxPoint> bounding_rect;
	bool save_background_ = true;
	bool detect_bounding_box_ = false;
public:

	CRender() : m_State(true), m_pGraphics(0)
	{
		m_LargeInt.QuadPart = 0;
		frameProc = CameraImageProcessingFactory::CreateBoundingBoxDetector(BackgroundFilterType::kStatic);

		m_hGlobal = GlobalAlloc(GMEM_MOVEABLE | GMEM_NODISCARD, GLOBAL_MEM);
		if (m_hGlobal == NULL)
		{
			m_State = false;
			return;
		}
		HRESULT hr = CreateStreamOnHGlobal(m_hGlobal, TRUE, &m_pStrm);
		if (hr != S_OK || m_pStrm == 0)
		{
			m_State = false;
			m_pStrm = 0;
			return;
		}
		GdiplusStartupInput gdiplusStartupInput;
		Status sta = GdiplusStartup(&m_gdiplusToken, &gdiplusStartupInput, NULL);
		if (sta != Ok)
		{
			m_State = false;
			return;
		}
	}

	~CRender()
	{
		if (m_pGraphics)
		{
			delete m_pGraphics;
			m_pGraphics = 0;
		}

		GdiplusShutdown(m_gdiplusToken);

		if (m_pStrm)
		{
			m_pStrm->Release();
			m_pStrm = 0;
		}
		if (m_hGlobal)
			GlobalFree(m_hGlobal);
	}

	void Attach(CStatic &ctrl)
	{
		ctrl.GetClientRect(&m_rcStatic);
		m_pGraphics = new Graphics(ctrl.GetDC()->m_hDC);
		if (m_pGraphics == 0)
			m_State = false;
	}

	void Render(LPBYTE MediaBuffer, LONG BufferSize)
	{
		if (MediaBuffer == NULL || BufferSize == 0)
		{
			std::cout << "Render() : Invalid Image data received..." << std::endl;
			return;
		}

		if (m_State)
		{
			HRESULT hr = m_pStrm->Seek(m_LargeInt, STREAM_SEEK_SET, NULL);
			if (hr != S_OK) return;
			hr = m_pStrm->Write(MediaBuffer, (ULONG)BufferSize, NULL);
			if (hr != S_OK) return;
			hr = m_pStrm->Seek(m_LargeInt, STREAM_SEEK_SET, NULL);
			if (hr != S_OK) return;

			Gdiplus::Image *pImage = new Gdiplus::Image(m_pStrm);

			if (pImage == 0) return;

			Status sta = m_pGraphics->DrawImage(pImage, 0, 0, m_rcStatic.Width(), m_rcStatic.Height());

				if (save_background_)
				{
					frameProc->SetBackgroundFrame((LPBYTE)MediaBuffer, BufferSize,pImage->GetWidth(), pImage->GetHeight(),0);
					save_background_ = false;
				}
				else
				{
					if (detect_bounding_box_)
					{
						bool rect_found = frameProc->DetectBoundingBox((LPBYTE)MediaBuffer, BufferSize, pImage->GetWidth(), pImage->GetHeight(),0, bounding_rect);
						if (rect_found)
						{
							Gdiplus::Pen* penCurrent = new Gdiplus::Pen(Color::LimeGreen, 3);
							int x_pos = (int) (((float)((float)bounding_rect[0].x / (float)pImage->GetWidth())) *(float)m_rcStatic.Width());
							int y_pos = (int) (((float)((float)bounding_rect[0].y / (float)pImage->GetHeight())) *(float)m_rcStatic.Height());

							int rect_width = (int) (((float)((float)(bounding_rect[3].x - bounding_rect[0].x) / (float)pImage->GetWidth()) *(float)m_rcStatic.Width()));
							int rect_height= (int) (((float)((float)(bounding_rect[3].y - bounding_rect[0].y) / (float)pImage->GetHeight())*(float)m_rcStatic.Height()));
							m_pGraphics->DrawRectangle(penCurrent, x_pos, y_pos, rect_width, rect_height);
						}
					}
				}
			delete pImage;
		}
	}

	void ClearImage()
	{
		Color cc(212, 208, 200);
		m_pGraphics->Clear(cc);
	}

	void SaveBackground()
	{
		save_background_ = true;
	}

	void DetectBoundingBox(bool detect_bounding_box)
	{
		detect_bounding_box_ = detect_bounding_box;
	}

private:

	bool m_State;
	HGLOBAL m_hGlobal;
	ULONG_PTR m_gdiplusToken;
	IStream *m_pStrm;
	CRect m_rcStatic;
	Graphics *m_pGraphics;
	LARGE_INTEGER m_LargeInt;

};

// class to Handle Continuous Image Events
class ContinuousImageEventHandler;

// class to Handle Snapshot Image Events
class SnapshotImageEventHandler;

// class to Handle Produce Image Events
class ProduceImageEventHandler;

// class to Handle Decode Image Events
class DecodeImageEventHandler;

// Class to handle decode session status change events
class DecodeSessionStatusChangeEventHandler;

// class to Handle Firmware download Events
class FirmwareDownloadEventHandler;


// CZebraCameraSDKSampleApplicationDlg dialog
class CZebraCameraSDKSampleApplicationDlg : public CDialogEx , public DeviceAttachedListener , public DeviceDetachedListener
{
// Construction
public:
	CZebraCameraSDKSampleApplicationDlg(CWnd* pParent = nullptr);	// standard constructor
	~CZebraCameraSDKSampleApplicationDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ZEBRACAMERASDKSAMPLEAPPLICATION_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	BOOL PreTranslateMessage(MSG* pMsg);
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

private:
	std::shared_ptr<ZebraCamera> camera_ = nullptr;
	std::vector<DeviceInfo> device_info_list_;
	DeviceInfo* selected_device_info_;
	bool b_UI_initialized_ = false;
	std::vector<uint8_t> image_buffer_;
	UVCProperty BrightnessPropValues = { 0 };
	UVCProperty ContrastPropValues = { 0 };
	UVCProperty SaturationPropValues = { 0 };
	UVCProperty SharpnessPropValues = { 0 };
	UVCProperty WhiteBalanceTempPropValues = { 0 };
	UVCProperty ExposurePropValues = { 0 };
	UVCProperty GammaPropValues = { 0 };
	UVCProperty GainPropValues = { 0 };
	UVCProperty BackLightCompPropValues = { 0 };
	UVCProperty WhiteBalanceComponentValues = { 0 };
	UVCProperty WhiteBalanceComponentRedValue = { 0 };
	UVCProperty WhiteBalanceComponentBlueValue = { 0 };
	TCHAR image_save_path_[MAX_PATH];

	ContinuousImageEventHandler* continuous_image_event_handler_ = nullptr;
	SnapshotImageEventHandler* snapshot_image_event_handler_ = nullptr;
	ProduceImageEventHandler* produce_image_event_handler_ = nullptr;
	DecodeImageEventHandler* decode_image_event_handler_ = nullptr;
	DecodeSessionStatusChangeEventHandler* decode_session_status_change_event_handler_ = nullptr;
	FirmwareDownloadEventHandler* firmware_download_event_handler_ = nullptr;

	ZebraCameraManager camera_manager_;
	DeviceManager device_manager_;
	FileConverter converter_type_;
	TargetFileFormat target_file_format_= TargetFileFormat::BMP;
	std::vector<FrameType> supported_frame_types_;
	int selected_frame_type_item = 0;
	CString m_edit_SerialNo_value_;
	CSliderCtrl m_slider_brightness_;
	int m_slider_brightness_value_;
	CEdit m_edit_brightness_;
	CString m_edit_brightness_value_;
	CSliderCtrl m_slider_contrast_;
	int m_slider_contrast_value_;
	CEdit m_edit_contrast_;
	CString m_edit_contrast_value_;
	CSliderCtrl m_slider_hue_;
	int m_slider_hue_value_;
	CString m_edit_hue_value_;
	CEdit m_edit_hue_;
	CSliderCtrl m_slider_saturation_;
	int m_slider_saturation_value_;
	CEdit m_edit_saturation_;
	CString m_edit_saturation_value_;
	CSliderCtrl m_slider_sharpness_;
	int m_slider_sharpness_value_;
	CEdit m_edit_sharpness_;
	CString m_edit_sharpness_value_;
	CEdit m_edit_whitebalance_;
	CString m_edit_whitebalance_value_;
	CButton m_cbox_whitebalanceAuto_;
	BOOL m_cbox_whitebalanceAuto_value_;
	CEdit m_edit_serialNo_;
	CEdit m_edit_modelnumber_;
	CString m_edit_modelnumber_value_;
	CEdit m_edit_dateofmanufacture_;
	CString m_edit_dateofmanufacture_value_;
	CEdit m_edit_dateofservice_;
	CString m_edit_dateofservice_value_;
	CEdit m_edit_dateoffirstprogram_;
	CString m_edit_dateoffirstprogram_value_;
	CEdit m_edit_firmwareversion_;
	CString m_edit_firmwareversion_value_;
	CEdit m_edit_hardwareversion_;
	CString m_edit_hardwareversion_value_;
	LRESULT CZebraCameraSDKSampleApplicationDlg::OnUpdateData(WPARAM a, LPARAM b);
	LRESULT CZebraCameraSDKSampleApplicationDlg::OnCameraAttached(WPARAM wparam, LPARAM lparam);
	LRESULT CZebraCameraSDKSampleApplicationDlg::OnCameraDetached(WPARAM wparam, LPARAM lparam);
	LRESULT UpdateDecodeSessionStatusImage(WPARAM wparam, LPARAM lparam);

	void UpdateDataEx();
	CButton m_button_setdefaults_;
	CButton m_button_reboot_;
	CButton m_button_capture_;
	CButton m_check_illumination_capture_;
	CComboBox* m_combo_camera_list_;
	CComboBox* m_combo_illuminationMode_;
	CComboBox* m_combo_videoMode_;
	CComboBox* m_combo_imageFormat_;
	CComboBox* m_combo_powerUserMode_;
	CComboBox* m_combo_frame_types_;
	int m_selected_camera_list_index_;
	std::string m_selected_camera_device_path_;
	CButton m_button_browse_;
	CButton m_button_save_;
	
	CButton m_check_autosaveimage_;
	BOOL m_check_autosaveimage_value_;
	BOOL m_button_saveButtonPressed_;
	CStatic m_static_displayimge_;
	CStatic m_picturecontrol_image_;
	CButton m_check_picklist_image_;
	BOOL m_check_picklist_image_value_;
	CButton m_check_snapshot_image_event_;
	BOOL m_check_snapshot_image_event_value_;
	CButton m_check_decode_image_event_;
	BOOL m_check_decode_image_event_value_;
	CButton m_check_continuous_image_event_;
	BOOL m_check_continuous_image_event_value_;
	CEdit m_edit_eventType_;
	CString m_edit_eventType_value_;
	CEdit m_edit_size_;
	CString m_edit_size_value_;
	CEdit m_edit_format_;
	CString m_edit_format_value_;
	CEdit m_edit_timestamp_;
	CString m_edit_timestamp_value_;
	CRender m_RenderEngine_;
	HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
	CBrush  m_brush_;
	CEdit m_edit_saveLocation_;
	CString m_edit_saveLocation_value_;
	BOOL CheckSaveImageLocation();
	void ResetImageEventRegistration();
	CRITICAL_SECTION CriticalSection;
	CButton m_check_white_balance_temp_auto_;
	BOOL m_check_white_balance_temp_auto_value_;
	CEdit m_edit_fwFilePath_;
	CString m_edit_fwFilePath_value_;
	CButton m_button_fwFileBrowse_;
	CButton m_button_fwDownload_;
	CButton m_button_fwInstall_;
	CButton m_button_fwCancel_;
	CProgressCtrl m_progressbar_fwDownload_;
	CEdit m_edit_exposure_;
	CString m_edit_exposure_value_;
	CSliderCtrl m_slider_exposure_;
	int m_slider_exposure_value_;
	CButton m_check_exposure_auto_;
	BOOL m_check_exposure_auto_value_;
	CEdit m_edit_imge_resolution_;
	CEdit m_edit_decode_data_;
	CString m_edit_image_resolution_value_;
	CString m_edit_decode_data_value_;
	CSliderCtrl m_slider_gamma_;
	CEdit m_edit_gamma_;
	int m_slider_gamma_value_;
	CString m_edit_gamma_value_;
	CSliderCtrl m_slider_gain_;
	int m_slider_gain_value_;
	CEdit m_edit_gain_;
	CString m_edit_gain_value_;
	CButton m_button_write_to_flash_;
	CSliderCtrl m_slider_backlight_comp_;
	int m_slider_backlight_comp_value_;
	CEdit m_edit_backlight_comp_;
	CString m_edit_backlight_comp_value_;
	// White balance component - red slider
	CSliderCtrl m_slider_wb_component_red_;
	// White balance component red slider value
	int m_slider_wb_component_red_value_;
	// White balance component - blue slider
	CSliderCtrl m_slider_wb_component_blue_;
	// White balance component blue slider value
	int m_slider_wb_component_blue_value_;
	CEdit m_edit_wb_component_red_;
	CString m_edit_wb_component_red_value_;
	CString m_edit_wb_blue_temp_value_;
	CString m_edit_wb_red_temp_value_;
	CButton m_button_wbc_get_temp_;
	CButton m_button_wbc_set_temp_;
	CEdit m_edit_wb_blue_temp_;
	CEdit m_edit_wb_red_temp_;
	CEdit m_edit_log_;
	CString m_edit_log_value_;
	CButton m_button_clear_log_;
	BOOL m_button_clear_logButtonPressed_;
	CButton m_button_load_config_;
	CButton m_button_retrieve_config_;
	CButton m_check_decode_session_status_change_event_;
	BOOL m_check_decode_session_status_change_event_value_;
	CStatic m_device_awake_status_image_;
	CBitmap m_device_awake_bmp_;
	CBitmap m_device_sleep_bmp_;
	CBitmap m_device_awake_status_bmp_;

	friend class ContinuousImageEventHandler;
	friend class SnapshotImageEventHandler;
	friend class ProduceImageEventHandler;
	friend class DecodeImageEventHandler;
	friend class DecodeSessionStatusChangeEventHandler;
	friend class FirmwareDownloadEventHandler;

	afx_msg void OnBnClickedButtonWriteToFlash();
	afx_msg void OnBnClickedButtonwbget();
	afx_msg void OnBnClickedButtonwbset();
	afx_msg void OnBnClickedButtonSetdefaults();
	afx_msg void OnBnClickedButtonReboot();
	afx_msg void OnBnClickedButtonCapture();
	afx_msg void OnCbnSelchangeComboCameraList();
	afx_msg void OnBnClickedButtonLoadConfig();
	afx_msg void OnBnClickedButtonRetrieveConfig();
	afx_msg void OnBnClickedClear();
	afx_msg void OnCbnSelchangeComboImageFormat();
	afx_msg void OnBnClickedCheckExposureAuto();
	afx_msg void OnCbnSelchangeComboPowerUserMode();
	afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	afx_msg void OnCbnSelchangeComboVideoMode();
	afx_msg void OnBnClickedButtonFwDownload();
	afx_msg void OnBnClickedButtonFwInstall();
	afx_msg void OnBnClickedButtonFwCancel();
	afx_msg void OnBnClickedCheckAutoSaveImage();
	afx_msg void OnBnClickedCheckContinuousImageEvent();
	afx_msg void OnBnClickedCheckSnapshotImageEvent();
	afx_msg void OnBnClickedCheckProduceImageEvent();
	afx_msg void OnBnClickedCheckDecodeImageEvent();
	afx_msg void OnBnClickedCheckDecodeSessionStatusChangeEvent();
	afx_msg void OnClose();
	afx_msg void OnBnClickedCheckWhitebalancetempAuto();
	afx_msg void OnCbnSelchangeComboIlluminationMode();
	afx_msg void OnCbnSelchangeComboFrametypes();
	afx_msg void OnBnClickedButtonFwBrowse();
	afx_msg void OnBnClickedButtonBrowse();
	afx_msg void OnBnClickedButtonSaveImage();

	void UpdateCameraDetails();
	void UpdateCameraProperties();
	void UpdateSliderControl(UVCProperty* propertyValue, CSliderCtrl* sliderCtrl, int* sliderValue, CString* sliderValueText, CButton* autoCheckCtrl, BOOL* autoValue);
	void UpdateCameraAdapter();
	void UpdateCameraList();
	void ClearData();
	void EnableDisableControls(BOOL enableFlag);
	void Attached(DeviceInfo info) override;
	void Detached(DeviceInfo info) override;
	void EnableCotrolOnPowerUserMode(bool flag);
	double WBFixedToDouble(uint16_t val);
	uint16_t WBDoubleToFixed(double val);
	void UpdateBrightness();
	void UpdateContrast();
	void UpdateSaturation();
	void UpdateSharpness();
	void UpdateWhitebalance();
	void UpdateExposure();
	void UpdateGamma();
	void UpdateGain();
	void UpdateBacklightCompensation();
	void CheckIlluminationMode();
	void CheckVideoMode();
	void CheckPowerUserMode();
	void ShowPropertyException(CString property_name, const std::exception& e);
	CString GetFileName(std::string temp_decode_data);
	void SetDeviceAwakeStatusImage(DecodeSessionStatus status);
	void LogCameraAssetInfo(std::shared_ptr< ZebraCamera> camera_);
	void ShowImage(std::vector<uint8_t>& image, std::string decode_data, uint32_t width, uint32_t height, size_t size);
	void SaveImage(std::vector<uint8_t>* imageBuffer, CString file_name);
	void ExecuteRebootProcedures();
	void ChangeResolutionsCbEnableState();
	void HandleImage(ImageData& evdat, ImageFormat& format, std::string decode_data = "");
	zebra::image::FileConverter GetFileConverter(ImageFormat& format);
	void EventLog(const char * eventDescription);
	void EventLog(const char * eventDescription, int value);
public:
	BOOL m_detect_bounding_box_;
	afx_msg void OnBnClickedCheckDetectBoundingBox();
	afx_msg void OnBnClickedButtonSetBackground();
	CButton m_check_detect_bounding_box_;
	CButton m_button_set_background_;
};

//class to handle slider controls
template <typename T , CameraPropertyId ID >
class SliderControl {
public:
	CSliderCtrl* m_slider_ctrl_;
	int* m_slider_value_;
	CEdit* m_edit_ctrl_;
	CString* m_edit_value_; 
	CameraPropertyId propID_;
	Property<T , ID>* property_;
	PropertyAuto<T, ID>* propAuto_;
	int step_value_;

	SliderControl(CSliderCtrl* sctrl, int* svalue , CEdit* ectrl, CString* estr , Property< T,  ID >* prop , CameraPropertyId propID, int step)
		: m_slider_ctrl_(sctrl)
		, m_slider_value_(svalue)
		, m_edit_ctrl_(ectrl)
		, m_edit_value_(estr)
		, propID_(propID)
		, step_value_(step)
	{
		property_ = prop;
		propAuto_ = nullptr;
	}

	SliderControl(CSliderCtrl* sctrl, int* svalue, CEdit* ectrl, CString* estr, PropertyAuto< T, ID >* prop, CameraPropertyId propID, int step)
		: m_slider_ctrl_(sctrl)
		, m_slider_value_(svalue)
		, m_edit_ctrl_(ectrl)
		, m_edit_value_(estr)
		, propID_(propID)
		, step_value_(step)
	{
		propAuto_ = prop;
		property_ = nullptr;
	}

	void HandleSliderControl(int prevValue);
	void HandleSliderControl(int prevValue , bool isAuto);
};

// class to Handle Continuous Image Events 
class ContinuousImageEventHandler :public ContinuousImageEventListener
{
public:
	ContinuousImageEventHandler(CZebraCameraSDKSampleApplicationDlg* pdlg) :m_pDlg_(pdlg) {}
	void ImageReceived(ImageEventData ev, ImageEventMetaData evm) override;

private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};


// class to Handle Snapshot Image Events 
class SnapshotImageEventHandler :public SnapshotImageEventListener
{
public:
	SnapshotImageEventHandler(CZebraCameraSDKSampleApplicationDlg* pdlg) :m_pDlg_(pdlg) {}
	void ImageReceived(ImageEventData ev, ImageEventMetaData evm) override;
	
private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};

// class to Handle Produce Image Events 
class ProduceImageEventHandler :public ProduceImageEventListener
{
public:
	ProduceImageEventHandler(CZebraCameraSDKSampleApplicationDlg* pdlg) :m_pDlg_(pdlg) {}
	void ImageReceived(ImageEventData ev, ImageEventMetaData evm) override;
	

private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};


// class to Handle Produce Image Events 
class DecodeImageEventHandler :public DecodeImageEventListener
{
public:
	DecodeImageEventHandler(CZebraCameraSDKSampleApplicationDlg* pdlg) :m_pDlg_(pdlg) {}
	void ImageReceived(ImageEventData ev, ImageEventMetaData evm) override;
	

private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};

// Class to handle decode session status change events
class DecodeSessionStatusChangeEventHandler :public DecodeSessionStatusChangeEventListener
{
public:
	DecodeSessionStatusChangeEventHandler(CZebraCameraSDKSampleApplicationDlg* dlg) :m_pDlg_(dlg) {}
	void DecodeSessionStatusChanged(DecodeSessionStatus status) override;

private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};

// Class to handle firmware download events 
class FirmwareDownloadEventHandler :public FirmwareDownloadEventListener
{
public:
	FirmwareDownloadEventHandler(CZebraCameraSDKSampleApplicationDlg* dlg) :m_pDlg_(dlg) {}
	void EventReceived(FirmwareDownloadEventsArgs evargs) override;

private:
	CZebraCameraSDKSampleApplicationDlg* m_pDlg_;
};