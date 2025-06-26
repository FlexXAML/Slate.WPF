﻿using Microsoft.UI.Xaml;
using System;

namespace Slate.WinUI3
{
    public partial class SlateFluent
    {
        public SlateFluent Window<T>() where T : Window
        {
            _register.RegisterMap["SlateFrameworkWindow"] = typeof (T);
            return this;
        }

        public SlateFluent Window<T>(Func<T> window) where T : Window
        {
            _register.RegisterMap["SlateFrameworkWindow"] = typeof (T);
            return this;
        }

        public SlateFluent DefineNestedLayout<T>()
        {
            _register.NestedLayout = typeof (T);
            return this;
        }

        public SlateFluent DefineNestedLayout<T>(Func<T> content)
        {
            _register.NestedLayout = typeof (T);
            return this;
        }

        public SlateFluent StartWithLayout<T>()
        {
            if (_register.InitialLayout != null)
                throw new InvalidOperationException ("초기 Layout은 이미 설정되었습니다.");
            _register.InitialLayout = typeof (T);
            return this;
        }

        public SlateFluent StartWithLayout<T>(Func<T> content)
        {
            if (_register.InitialLayout != null)
                throw new InvalidOperationException ("초기 Layout은 이미 설정되었습니다.");
            _register.InitialLayout = typeof (T);
            return this;
        }
    }
}
