using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataTemplateSelectors
{
    class NewsDataS : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Types.wall_post p = (Types.wall_post)item;
            FrameworkElement element = container as FrameworkElement;
            switch (p.type)
            {
                case "post":
                    return (DataTemplate)element.FindResource("WallPostTemplate");
                case "photo":
                    return (DataTemplate)element.FindResource("PhotoTemplate");
                case "photo_tag":
                    return (DataTemplate)element.FindResource("PhotoTagTemplate");
                case "wall_photo":
                    return (DataTemplate)element.FindResource("WallPhotoTemplate");
                case "friend":
                    return (DataTemplate)element.FindResource("FriendTemplate");
                default:
                    return null;
            }
        }
    }
    class AttachmentDataS : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            bool isView = false;//(container as FrameworkElement).TemplatedParent is FlipView;
            if (item is Types.attachment)
            {
                Types.attachment a = (Types.attachment)item;
                if (a == null)
                {
                    return null;
                }
                switch (a.type)
                {
                    case "photo":
                        return (DataTemplate)element.FindResource(isView ? "PhotoViewAttachment" : "PhotoAttachment");
                    case "audio":
                        return (DataTemplate)element.FindResource("AudioAttachment");
                    case "poll":
                        return (DataTemplate)element.FindResource("PollAttachment");
                    case "doc":
                        return (DataTemplate)element.FindResource("DocAttachment");
                    case "sticker":
                        return (DataTemplate)element.FindResource("StickerAttachment");
                    case "link":
                        return (DataTemplate)element.FindResource("LinkAttachment");
                    case "video":
                        return (DataTemplate)element.FindResource("VideoAttachment");
                    case "wall":
                        if (container is ContentPresenter)
                        {
                            (container as ContentPresenter).Content = a.wall;
                        }
                        return null;
                    default:
                        return null;
                }
            }
            else if (item is Types.wall_post)
            {
                Types.wall_post w = item as Types.wall_post;
                if (w.post_type == "post")
                {
                    return (DataTemplate)element.FindResource("WallPostTemplate");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
