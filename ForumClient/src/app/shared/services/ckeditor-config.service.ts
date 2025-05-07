import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { CKEditorCloudConfig, CKEditorCloudResult, loadCKEditorCloud } from '@ckeditor/ckeditor5-angular';
import { ClassicEditor, EditorConfig } from 'ckeditor5';
import { isPlatformBrowser } from '@angular/common';
import { log } from 'console';

const LICENSE_KEY =
    'eyJhbGciOiJFUzI1NiJ9.eyJleHAiOjE3NjU5Mjk1OTksImp0aSI6IjE0YTJlODgwLTY5NjEtNDk1Ni05MGJiLWRhOWQyMTRhZTdjMiIsInVzYWdlRW5kcG9pbnQiOiJodHRwczovL3Byb3h5LWV2ZW50LmNrZWRpdG9yLmNvbSIsImRpc3RyaWJ1dGlvbkNoYW5uZWwiOlsiY2xvdWQiLCJkcnVwYWwiXSwiZmVhdHVyZXMiOlsiRFJVUCIsIkJPWCJdLCJ2YyI6ImU4NjBiOTIxIn0.MOCFS5Hh0ctp8a8ovvpTxYrVsNbTpzxSnl5qiw-uwmmjQ8sz-yw7T0Qj9pmrANpSgMDAsyFTivbjq7URckTu6g';
const cloudConfig = {
    version: '44.1.0',
    translations: ['vi']
} satisfies CKEditorCloudConfig;

@Injectable({
    providedIn: 'root'
})
export class CkeditorConfigService {
    platForm = inject(PLATFORM_ID);

    public Editor: typeof ClassicEditor | null = null;
    public config: EditorConfig | null = null;
    isComment = false;

    public constructor() {
        if (isPlatformBrowser(this.platForm))
            loadCKEditorCloud(cloudConfig).then(this._setupEditor.bind(this));
    }

    private _setupEditor(cloud: CKEditorCloudResult<typeof cloudConfig>) {
        const {
            ClassicEditor,
            Autoformat,
            Autosave,
            BalloonToolbar,
            Bold,
            Code,
            CodeBlock,
            Essentials,
            GeneralHtmlSupport,
            ImageEditing,
            ImageUtils,
            AutoImage,
            Base64UploadAdapter,
            ImageBlock,
            ImageCaption,
            ImageInline,
            ImageInsert,
            ImageInsertViaUrl,
            ImageResize,
            ImageStyle,
            ImageTextAlternative,
            ImageToolbar,
            ImageUpload,
            LinkImage,
            Italic,
            Link,
            Mention,
            Paragraph,
            PasteFromOffice,
            TextTransformation,
            Underline
        } = cloud.CKEditor;

        this.Editor = ClassicEditor;
        this.config = {
            toolbar: {
                items: ['bold', 'italic', 'underline', 'code', '|', 'link', 'codeBlock', 'insertImage'],
                shouldNotGroupWhenFull: false
            },
            plugins: [
                Autoformat,
                Autosave,
                BalloonToolbar,
                Bold,
                Code,
                CodeBlock,
                Essentials,
                GeneralHtmlSupport,
                ImageEditing,
                ImageUtils,
                AutoImage,
                Base64UploadAdapter,
                ImageBlock,
                ImageCaption,
                ImageInline,
                ImageInsert,
                ImageInsertViaUrl,
                ImageResize,
                ImageStyle,
                ImageTextAlternative,
                ImageToolbar,
                ImageUpload,
                LinkImage,
                Italic,
                Link,
                Mention,
                Paragraph,
                PasteFromOffice,
                TextTransformation,
                Underline
            ],
            balloonToolbar: ['bold', 'italic', '|', 'link', 'insertImage'],
            htmlSupport: {
                allow: [
                    {
                        name: /^.*$/,
                        styles: true,
                        attributes: true,
                        classes: true
                    }
                ]
            },
            language: 'vi',
            licenseKey: LICENSE_KEY,
            link: {
                addTargetToExternalLinks: true,
                defaultProtocol: 'https://',
                decorators: {
                    toggleDownloadable: {
                        mode: 'manual',
                        label: 'Downloadable',
                        attributes: {
                            download: 'file'
                        }
                    }
                }
            },
            image: {
                toolbar: [
                    'toggleImageCaption',
                    'imageTextAlternative',
                    '|',
                    'imageStyle:inline',
                    'imageStyle:wrapText',
                    'imageStyle:breakText',
                    '|',
                    'resizeImage'
                ]
            },
            mention: {
                feeds: [
                    {
                        marker: '@',
                        feed: [
                            /* See: https://ckeditor.com/docs/ckeditor5/latest/features/mentions.html */
                        ]
                    }
                ]
            },
            placeholder: 'Nhập nội dung topic tại đây!'
        };
    }
}
