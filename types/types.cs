using FirstFloor.ModernUI.Presentation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TinyClient.Api;

namespace Types
{
    public class audio
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int album_id { get; set; }
        public int genre_id { get; set; }
        public string full_title {
            get { return artist.Trim() + " - " + title.Trim(); }
        }
    }

    public class user
    {
        public int id { get; set; }
        public string deactivated { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_100 { get; set; }
        public int sex { get; set; }

        public string full_name {
            get { return first_name + " " + last_name; }
        }
    }

    public class profile
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
        public string university_name { get; set; }
        public string can_write_private_message { get; set; }
        public string deactivated { get; set; }
        public int sex { get; set; }
        public bool online_mobile { get; set; }
        public string online_app { get; set; }
        public bool online { get; set; }
        public last_seen last_seen { get; set; }
        public string full_name {
            get { return first_name + " " + last_name; }
        }
    }
    public class last_seen
    {
        public int time { get; set; }
        public int platform { get; set; }
    }
	class @group
	{
		public int id { get; set; }
		public string name { get; set; }
		public string photo_50 { get; set; }
	}
	
	class GetUserInfo
	{
		public user_info user { get; set; }
		public lastActivity lastActivity { get; set; }
	}
	
	class lastActivity
	{
		public bool online { get; set; }
		public string time { get; set; }
	}
	class user_info : user
	{
		public string photo_200_orig { get; set; }
		public string nickname { get; set; }
		public string bdate { get; set; }
		public city city { get; set; }
		public string home_town { get; set; }
		public string mobile_phone { get; set; }
		public string home_phone { get; set; }
		public string skype { get; set; }
		public string site { get; set; }
		public string university_name { get; set; }
		public string faculty_name { get; set; }
		public string education_form { get; set; }
		public int graduation { get; set; }
		public List<university> universities { get; set; }
		public personal personal { get; set; }
		public string activities { get; set; }
		public string interests { get; set; }
		public string music { get; set; }
		public string movies { get; set; }
		public string tv { get; set; }
		public string books { get; set; }
		public string games { get; set; }
		public string about { get; set; }
		public string quotes { get; set; }
		public bool can_write_private_message { get; set; }
		public counters counters { get; set; }
		public bool online { get; set; }
		public bool online_mobile { get; set; }
		public string online_app { get; set; }
	}
	
	class VKError
	{
		public int error_code { get; set; }
		public string error_msg { get; set; }
		public request_param[] request_params { get; set; }
	}


    public class message : SetDockAttachments
    {
        private bool _isSuccessfulSending1 = true;
        private bool _readState;
        private bool _isError1;

        private bool _isSend1;
        public int id
        {
            get { return _id1; }
            set
            {
                if (LongPollServerParser.MaxMsgId < value)
                {
                    LongPollServerParser.MaxMsgId = value;

#if DEBUG
                    Debug.Print(LongPollServerParser.MaxMsgId + " " + value);
#endif
                }
                _id1 = value;
            }
        }

        public int user_id { get; set; }
        public string body { get; set; }
        public bool @out { get; set; }

        public bool not_out
        {
            get { return !@out; }
        }

        public bool read_state
        {
            get { return _readState; }
            set
            {
                _readState = value;
                OnPropertyChanged("read_state");
            }
        }

        public int chat_id { get; set; }
        public string title { get; set; }
        public List<message> fwd_messages { get; set; }
        public bool emoji { get; set; }

        public bool IsSend
        {
            get { return _isSend1; }
            set
            {
                _isSend1 = value;
                OnPropertyChanged("IsSend");
            }
        }

        public bool IsActive { get; set; }
        public string date { get; set; }
        public string action { get; set; }
        public string action_text { get; set; }
        public string action_email { get; set; }
        public string action_mid { get; set; }

        public bool IsSuccessfulSending
        {
            get { return _isSuccessfulSending1; }
            set
            {
                _isSuccessfulSending1 = value;
                OnPropertyChanged("IsSuccessfulSending");
            }
        }

        public bool IsError
        {
            get { return _isError1; }
            set
            {
                _isError1 = value;
                OnPropertyChanged("IsError");
            }
        }

        public string Tag { get; set; }
        private string _key;
        private int _id1;

        private bool _important1;
        public bool important
        {
            get { return _important1; }
            set
            {
                _important1 = value;
                OnPropertyChanged("important");
            }
        }

        public string Key
        {
            get
            {
                if (String.IsNullOrEmpty(_key))
                {
                    _key = chat_id > 0 ? "c" + chat_id : user_id.ToString();
                }
                return _key;
            }
        }
    }

    public class attachment
	{
		public string type { get; set; }
		public photo photo { get; set; }
		public video video { get; set; }
		public audio audio { get; set; }
		public linkvk link { get; set; }
		public sticker sticker { get; set; }
		public wall_post wall { get; set; }
		public doc doc { get; set; }
		public poll poll { get; set; }
		public Dock Dock { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
	}

	public class wall_post : SetDockAttachments
	{
		public photo_list photos { get; set; }
		public photo_list photo_tags { get; set; }
		public string source_name { get; set; }
		public bool IsMessage { get; set; }
		private string _name1;
		private int _sourceId;
		private int _postId;
		public string type { get; set; }
	
		public int source_id {
			get { return _sourceId; }
			set {
				_sourceId = value;
				from_id = value;
			}
		}
	
		public vk_list_user friends { get; set; }
		public string date { get; set; }
		public wall_post[] copy_history { get; set; }
		public string copy_owner_id { get; set; }
		public int from_id { get; set; }
	
		public int post_id {
			get { return _postId; }
			set {
				id = value;
				_postId = value;
			}
		}
	
		public string from_name { get; set; }
	
		internal void SetFromName(string fname)
		{
			from_name = fname;
			OnPropertyChanged("from_name");
		}
	
		public int id { get; set; }
		public bool IsCopyHistory { get; set; }
		public likes likes { get; set; }
		public int owner_id { get; set; }
		public string owner_name { get; set; }
		public string post_type { get; set; }
		public string text { get; set; }
		public int to_id { get; set; }
	
		public string name {
			get {
				if (IsCopyHistory && post_type == "photo") {
					return owner_name;
				} else {
					return from_name;
				}
			}
			set { _name1 = value; }
		}
	}

	public class poll
	{
		public int answer_id { get; set; }
		public answer[] answers { get; set; }
		public int created { get; set; }
		public int id { get; set; }
		public int owner_id { get; set; }
		public string question { get; set; }
		public int votes { get; set; }
	}
	
	public class answer
	{
		public int id { get; set; }
		public string text { get; set; }
		public int votes { get; set; }
		public double rate { get; set; }
	}
	
	public class sticker
	{
		public int id { get; set; }
		public string photo_64 { get; set; }
		public string photo_128 { get; set; }
		public string photo_256 { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}
	
	public class doc
	{
		public int id { get; set; }
		public int owneer_id { get; set; }
		public string title { get; set; }
		public int size { get; set; }
		public string ext { get; set; }
		public string url { get; set; }
		public string photo_100 { get; set; }
		public string photo_130 { get; set; }
	}
	
	public class linkvk
	{
		public string description { get; set; }
		public photo photo { get; set; }
		public string preview_page { get; set; }
		public string title { get; set; }
		public string url { get; set; }
	}
	
	public class photo : AttachmentHelper
	{
		public int id { get; set; }
		public int album_id { get; set; }
		public int owner_id { get; set; }
		public string photo_75 { get; set; }
		public string photo_130 { get; set; }
		public string photo_604 { get; set; }
		public string photo_807 { get; set; }
		public string photo_1280 { get; set; }
		public string photo_2560 { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string text { get; set; }
		public string date { get; set; }
	}
    
	public class AttachmentHelper
	{
		public string UpdatePhotoFromSize(Size avariableSize)
		{
            var url = "";
			if (this is photo) {
				var photo = this as photo;
                int size = Common.PhotoSizes[TinyClient.Properties.Settings.Default.photoSize];
                var maxSize = Math.Max(avariableSize.Width, avariableSize.Height);
				if (maxSize <= 75 && !String.IsNullOrEmpty(photo.photo_75)) {
					url = photo.photo_75;
				} else if (maxSize <= 130 && !String.IsNullOrEmpty(photo.photo_130)) {
					url = photo.photo_130;
				} else if (maxSize <= 604 && !String.IsNullOrEmpty(photo.photo_604)) {
					url = photo.photo_604;
				} else if (maxSize <= 807 && !String.IsNullOrEmpty(photo.photo_807)) {
					url = photo.photo_807;
				} else if (maxSize <= 1280 && !String.IsNullOrEmpty(photo.photo_1280)) {
					url = photo.photo_1280;
				} else if (maxSize <= 2560 && !String.IsNullOrEmpty(photo.photo_2560)) {
					url = photo.photo_2560;
				} else {
					url = GetPhotoMaxSize();
				}
			}
			return url;
		}
	
		public string GetPhotoMaxSize()
		{
            var url = "";
			if (this is photo) {
				dynamic photo = this as photo;
				if (!photo.photo_2560.IsNullOrEmpty()) {
					url = photo.photo_2560;
				} else if (!photo.photo_1280.IsNullOrEmpty()) {
					url = photo.photo_1280;
				} else if (!photo.photo_807.IsNullOrEmpty()) {
					url = photo.photo_807;
				} else if (!photo.photo_604.IsNullOrEmpty()) {
					url = photo.photo_604;
				} else if (!photo.photo_130.IsNullOrEmpty()) {
					url = photo.photo_130;
				} else if (!photo.photo_75.IsNullOrEmpty()) {
					url = photo.photo_75;
				}
			}
			return url;
		}
	}
	public class video
	{
		public int id { get; set; }
		public int owner_id { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public int duration { get; set; }
		public string link { get; set; }
		public string photo_130 { get; set; }
		public string photo_320 { get; set; }
		public string photo_640 { get; set; }
		public int date { get; set; }
		public int views { get; set; }
		public int comments { get; set; }
		public string player { get; set; }
		public string access_key { get; set; }
	}
	
	public class post_news : SetDockAttachments
	{
		public string type { get; set; }
		public photo_list photos { get; set; }
		public string source_name { get; set; }
		public int source_id { get; set; }
		public int date { get; set; }
		public int post_id { get; set; }
	
		public photo_list photo_tags { get; set; }
		public string text { get; set; }
		public vk_list_user friends { get; set; }
		public likes likes { get; set; }
		public copy_post copy_history1 { get; set; }
	
		private copy_post[] _copy_history;
		public copy_post[] copy_history {
			get { return _copy_history; }
			set { copy_history1 = value.FirstOrDefault(); }
		}
	
		public comment comments { get; set; }
	}
	
	public class comment
	{
		public int count { get; set; }
		public bool can_post { get; set; }
	}
	
	public class vk_list_user
	{
		public int count { get; set; }
		public List<vk_list_user_uid> items { get; set; }
	}
	
	public class vk_list_user_uid
	{
		public int user_id { get; set; }
		public string photo { get; set; }
	}
	
	public class copy_post : SetDockAttachments
	{
		public string type { get; set; }
		public int source_id { get; set; }
		public int owner_id { get; set; }
		public int date { get; set; }
		public int post_id { get; set; }
		public string text { get; set; }
		public string owner_name { get; set; }
	}
	
	public class likes : NotifyPropertyChanged
	{
		private bool _user_likes;
	
		private int _count1;
		public int count {
			get { return _count1; }
			set {
				_count1 = value;
				OnPropertyChanged("count");
			}
		}
	
		public bool user_likes {
			get { return _user_likes; }
			set {
				_user_likes = value;
				OnPropertyChanged("user_likes");
			}
		}
	
		public bool can_like { get; set; }
		public bool can_publish { get; set; }
	}
	
	public class photo_list
	{
		public int count { get; set; }
		public List<photo> items { get; set; }
	}
	
	public class app_list
	{
		public int count { get; set; }
		public List<app> items { get; set; }
	}
    public class app {
        public int members_count { get; set; }
    }
    class vk_list
	{
		public int count { get; set; }
	
		private int _unread;
		public int unread {
			get { return _unread; }
			set {
				if ((DialogTabItem != null)) {
					if (value > 0) {
						DialogTabItem.Unread = "+" + value;
						DialogTabItem.Title = DialogTabItem.RawTitle + " (+" + value + ")";
					} else {
						DialogTabItem.Unread = "0";
						value = 0;
						DialogTabItem.Title = DialogTabItem.RawTitle;
					}
				}
				_unread = value;
			}
		}
	
		//public ObservableCollection<types.message> items { get; set; }
		public MyTabItem DialogTabItem { get; set; }
		public ListBox ListBoxDialog { get; set; }
	}
	
	class LongPollServer
	{
		public string key { get; set; }
		public string server { get; set; }
		public string ts { get; set; }
	}
	
	class LongPoolServerUpdates
	{
		public string ts { get; set; }
		public int failed { get; set; }
		public string pts { get; set; }
		public JArray updates { get; set; }
	}
	
	class request_param
	{
		public string key { get; set; }
		public string value { get; set; }
	}
	
	class Notifycation
	{
		public int Index;
		public message Message { get; set; }
		public int type { get; set; }
		public int user_id { get; set; }
		public int chat_id { get; set; }
		public string title { get; set; }
		public string text { get; set; }
		public string tag { get; set; }
		public string date { get; set; }
		public object mytag { get; set; }
	}
	class city
	{
		public int id { get; set; }
		public string title { get; set; }
	}
	
	class university
	{
		public int id { get; set; }
		public string name { get; set; }
		public string faculty_name { get; set; }
		public string chair_name { get; set; }
		public int graduation { get; set; }
	}
	class personal
	{
		private readonly string[] _masPolitical = {
			"",/*
			My.Resources.personalPolitical1,
			My.Resources.personalPolitical2,
			My.Resources.personalPolitical3,
			My.Resources.personalPolitical4,
			My.Resources.personalPolitical5,
			My.Resources.personalPolitical6,
			My.Resources.personalPolitical7,
			My.Resources.personalPolitical8,
			My.Resources.personalPolitical9*/
	
		};
		private readonly string[] _masPeopleMain = {
			"",/*
			My.Resources.personal1,
			My.Resources.personal2,
			My.Resources.personal3,
			My.Resources.personal4,
			My.Resources.personal5,
			My.Resources.personal6*/
	
		};
		private readonly string[] _masLifeMain = {
			"",/*
			My.Resources.personallife1,
			My.Resources.personallife2,
			My.Resources.personallife3,
			My.Resources.personallife4,
			My.Resources.personallife5,
			My.Resources.personallife6,
			My.Resources.personallife7,
			My.Resources.personallife8*/
	
		};
		private readonly string[] _masSmoking = {
			"",/*
			My.Resources.smoking1,
			My.Resources.smoking2,
			My.Resources.smoking3,
			My.Resources.smoking4,
			My.Resources.smoking5*/
	
		};
	
		private string _political;
		public string political {
			get { return _political; }
			set { _political = _masPolitical[Convert.ToInt32(value)]; }
		}
	
	
		private string _life_main;
		public string life_main {
			get { return _life_main; }
			set { _life_main = _masLifeMain[Convert.ToInt32(value)]; }
		}
	
	
		private string _people_main;
		public string people_main {
			get { return _people_main; }
			set { _people_main = _masPeopleMain[Convert.ToInt32(value)]; }
		}
	
	
		private string _smoking;
		public string smoking {
			get { return _smoking; }
			set { _smoking = _masSmoking[Convert.ToInt32(value)]; }
		}
	
	
		private string _alcohol;
		public string alcohol {
			get { return _alcohol; }
			set { _alcohol = _masSmoking[Convert.ToInt32(value)]; }
		}
	
		public string[] langs { get; set; }
		public string religion { get; set; }
		public string inspired_by { get; set; }
	}
	
	class counters
	{
		public int albums { get; set; }
		public int videos { get; set; }
		public int audios { get; set; }
		public int photos { get; set; }
		public int notes { get; set; }
		public int friends { get; set; }
		public int groups { get; set; }
		public int online_friends { get; set; }
		public int mutual_friends { get; set; }
		public int user_videos { get; set; }
		public int followers { get; set; }
		public int user_photos { get; set; }
		public int subscriptions { get; set; }
	}
	
	class UploadServer
	{
		public string upload_url { get; set; }
		public int album_id { get; set; }
		public int user_id { get; set; }
	}
	
	public class ResponseUploadServer
	{
		public string server { get; set; }
		public string photo { get; set; }
		public string hash { get; set; }
		public string user_id { get; set; }
		public string group_id { get; set; }
	}
	
	class Product
	{
		public int id { get; set; }
		public string type { get; set; }
		public bool purchased { get; set; }
		public bool active { get; set; }
		public bool promoted { get; set; }
		public string purchase_date { get; set; }
		public string title { get; set; }
		public string base_url { get; set; }
		public Stickers stickers { get; set; }
		public ListBox ListBox1 { get; set; }
	}
	
	class Stickers
	{
		public string base_url { get; set; }
		public int[] sticker_ids { get; set; }
	}
	
	public class LanguageItem
	{
		public string Culture { get; set; }
		public string Description { get; set; }
		public bool AutoTranslate { get; set; }
	}
	
	class Post : SetDockAttachments
	{
		public int id { get; set; }
		public int owner_id { get; set; }
		public int from_id { get; set; }
		public string date { get; set; }
		public string text { get; set; }
		public int reply_owner_id { get; set; }
		public int reply_post_id { get; set; }
		public bool friends_only { get; set; }
		public string post_type { get; set; }
		public likes likes { get; set; }
	}
	
	class dialog : NotifyPropertyChanged
	{
		private int _unread;
	
		private profile _user1;
		public int unread {
			get { return _unread; }
			set {
				_unread = value;
				OnPropertyChanged("unread");
				OnPropertyChanged("unread_string");
			}
		}
	
		public string unread_string {
			get { return "+" + unread; }
		}
	
		public message message { get; set; }
	
		public profile user {
			get { return _user1; }
			set {
				_user1 = value;
				OnPropertyChanged("user");
			}
		}
	}
	
	class wall
	{
		public List<wall_post> items { get; set; }
		public List<profile> profiles { get; set; }
		public @group[] groups { get; set; }
	}
    public class SetDockAttachments : NotifyPropertyChanged
    {

        private List<attachment> _attachments;
        public List<attachment> attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
    }
}