import { createRequire } from 'module';const require = createRequire(import.meta.url);
import {
  FormsModule,
  NG_VALUE_ACCESSOR
} from "./chunk-CZCOKEKU.js";
import {
  CommonModule
} from "./chunk-SCTJIED2.js";
import "./chunk-QPSOHVJE.js";
import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  NgModule,
  NgZone,
  Output,
  VERSION,
  forwardRef,
  require_operators,
  setClassMetadata,
  ɵɵNgOnChangesFeature,
  ɵɵProvidersFeature,
  ɵɵdefineComponent,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵdirectiveInject,
  ɵɵtemplate
} from "./chunk-SV3LDYHF.js";
import "./chunk-AQYIT73X.js";
import {
  __async,
  __spreadProps,
  __spreadValues,
  __toESM
} from "./chunk-YHCV7DAQ.js";

// node_modules/@ckeditor/ckeditor5-angular/fesm2022/ckeditor-ckeditor5-angular.mjs
var import_operators = __toESM(require_operators(), 1);

// node_modules/@ckeditor/ckeditor5-integrations-common/dist/index.js
function waitFor(callback, {
  timeOutAfter = 500,
  retryAfter = 100
} = {}) {
  return new Promise((resolve, reject) => {
    const startTime = Date.now();
    let lastError = null;
    const timeoutTimerId = setTimeout(() => {
      reject(lastError ?? new Error("Timeout"));
    }, timeOutAfter);
    const tick = () => __async(this, null, function* () {
      try {
        const result = yield callback();
        clearTimeout(timeoutTimerId);
        resolve(result);
      } catch (err) {
        lastError = err;
        if (Date.now() - startTime > timeOutAfter) {
          reject(err);
        } else {
          setTimeout(tick, retryAfter);
        }
      }
    });
    tick();
  });
}
var INJECTED_SCRIPTS = /* @__PURE__ */ new Map();
function injectScript(src, {
  attributes
} = {}) {
  if (INJECTED_SCRIPTS.has(src)) {
    return INJECTED_SCRIPTS.get(src);
  }
  const maybePrevScript = document.querySelector(`script[src="${src}"]`);
  if (maybePrevScript) {
    console.warn(`Script with "${src}" src is already present in DOM!`);
    maybePrevScript.remove();
  }
  const promise = new Promise((resolve, reject) => {
    const script = document.createElement("script");
    script.onerror = reject;
    script.onload = () => {
      resolve();
    };
    for (const [key, value] of Object.entries(attributes || {})) {
      script.setAttribute(key, value);
    }
    script.setAttribute("data-injected-by", "ckeditor-integration");
    script.type = "text/javascript";
    script.async = true;
    script.src = src;
    document.head.appendChild(script);
    const observer = new MutationObserver((mutations) => {
      const removedNodes = mutations.flatMap((mutation) => Array.from(mutation.removedNodes));
      if (removedNodes.includes(script)) {
        INJECTED_SCRIPTS.delete(src);
        observer.disconnect();
      }
    });
    observer.observe(document.head, {
      childList: true,
      subtree: true
    });
  });
  INJECTED_SCRIPTS.set(src, promise);
  return promise;
}
function injectScriptsInParallel(sources, props) {
  return __async(this, null, function* () {
    yield Promise.all(sources.map((src) => injectScript(src, props)));
  });
}
var INJECTED_STYLESHEETS = /* @__PURE__ */ new Map();
function injectStylesheet({
  href,
  placementInHead = "start",
  attributes = {}
}) {
  if (INJECTED_STYLESHEETS.has(href)) {
    return INJECTED_STYLESHEETS.get(href);
  }
  const maybePrevStylesheet = document.querySelector(`link[href="${href}"][rel="stylesheet"]`);
  if (maybePrevStylesheet) {
    console.warn(`Stylesheet with "${href}" href is already present in DOM!`);
    maybePrevStylesheet.remove();
  }
  const appendLinkTagToHead = (link) => {
    const previouslyInjectedLinks = Array.from(document.head.querySelectorAll('link[data-injected-by="ckeditor-integration"]'));
    switch (placementInHead) {
      case "start":
        if (previouslyInjectedLinks.length) {
          previouslyInjectedLinks.slice(-1)[0].after(link);
        } else {
          document.head.insertBefore(link, document.head.firstChild);
        }
        break;
      case "end":
        document.head.appendChild(link);
        break;
    }
  };
  const promise = new Promise((resolve, reject) => {
    const link = document.createElement("link");
    for (const [key, value] of Object.entries(attributes || {})) {
      link.setAttribute(key, value);
    }
    link.setAttribute("data-injected-by", "ckeditor-integration");
    link.rel = "stylesheet";
    link.href = href;
    link.onerror = reject;
    link.onload = () => {
      resolve();
    };
    appendLinkTagToHead(link);
    const observer = new MutationObserver((mutations) => {
      const removedNodes = mutations.flatMap((mutation) => Array.from(mutation.removedNodes));
      if (removedNodes.includes(link)) {
        INJECTED_STYLESHEETS.delete(href);
        observer.disconnect();
      }
    });
    observer.observe(document.head, {
      childList: true,
      subtree: true
    });
  });
  INJECTED_STYLESHEETS.set(href, promise);
  return promise;
}
function preloadResource(url, {
  attributes
} = {}) {
  if (document.head.querySelector(`link[href="${url}"][rel="preload"]`)) {
    return;
  }
  const link = document.createElement("link");
  for (const [key, value] of Object.entries(attributes || {})) {
    link.setAttribute(key, value);
  }
  link.setAttribute("data-injected-by", "ckeditor-integration");
  link.rel = "preload";
  link.as = detectTypeOfResource(url);
  link.href = url;
  document.head.insertBefore(link, document.head.firstChild);
}
function detectTypeOfResource(url) {
  switch (true) {
    case /\.css$/.test(url):
      return "style";
    case /\.js$/.test(url):
      return "script";
    default:
      return "fetch";
  }
}
var HEX_NUMBERS = new Array(256).fill("").map((_, index) => ("0" + index.toString(16)).slice(-2));
function uid() {
  const [r1, r2, r3, r4] = crypto.getRandomValues(new Uint32Array(4));
  return "e" + HEX_NUMBERS[r1 >> 0 & 255] + HEX_NUMBERS[r1 >> 8 & 255] + HEX_NUMBERS[r1 >> 16 & 255] + HEX_NUMBERS[r1 >> 24 & 255] + HEX_NUMBERS[r2 >> 0 & 255] + HEX_NUMBERS[r2 >> 8 & 255] + HEX_NUMBERS[r2 >> 16 & 255] + HEX_NUMBERS[r2 >> 24 & 255] + HEX_NUMBERS[r3 >> 0 & 255] + HEX_NUMBERS[r3 >> 8 & 255] + HEX_NUMBERS[r3 >> 16 & 255] + HEX_NUMBERS[r3 >> 24 & 255] + HEX_NUMBERS[r4 >> 0 & 255] + HEX_NUMBERS[r4 >> 8 & 255] + HEX_NUMBERS[r4 >> 16 & 255] + HEX_NUMBERS[r4 >> 24 & 255];
}
function uniq(source) {
  return Array.from(new Set(source));
}
function waitForWindowEntry(entryNames, config) {
  return __async(this, null, function* () {
    const tryPickBundle = () => entryNames.map((name) => window[name]).filter(Boolean)[0];
    return waitFor(() => {
      const result = tryPickBundle();
      if (!result) {
        throw new Error(`Window entry "${entryNames.join(",")}" not found.`);
      }
      return result;
    }, config);
  });
}
function filterObjectValues(obj, filter) {
  const filteredEntries = Object.entries(obj).filter(([key, value]) => filter(value, key));
  return Object.fromEntries(filteredEntries);
}
function filterBlankObjectValues(obj) {
  return filterObjectValues(obj, (value) => value !== null && value !== void 0);
}
function mapObjectValues(obj, mapper) {
  const mappedEntries = Object.entries(obj).map(([key, value]) => [key, mapper(value, key)]);
  return Object.fromEntries(mappedEntries);
}
function without(itemsToRemove, items) {
  return items.filter((item) => !itemsToRemove.includes(item));
}
function appendExtraPluginsToEditorConfig(config, plugins) {
  const extraPlugins = config.extraPlugins || [];
  return __spreadProps(__spreadValues({}, config), {
    extraPlugins: [...extraPlugins, ...plugins.filter((item) => !extraPlugins.includes(item))]
  });
}
function isSemanticVersion(version) {
  return !!version && /^\d+\.\d+\.\d+/.test(version);
}
function isCKCdnTestingVersion(version) {
  if (!version) {
    return false;
  }
  return ["nightly", "alpha", "internal", "nightly-", "staging"].some((testVersion) => version.includes(testVersion));
}
function isCKCdnVersion(version) {
  return isSemanticVersion(version) || isCKCdnTestingVersion(version);
}
function destructureSemanticVersion(version) {
  if (!isSemanticVersion(version)) {
    throw new Error(`Invalid semantic version: ${version || "<blank>"}.`);
  }
  const [major, minor, patch] = version.split(".");
  return {
    major: Number.parseInt(major, 10),
    minor: Number.parseInt(minor, 10),
    patch: Number.parseInt(patch, 10)
  };
}
function getLicenseVersionFromEditorVersion(version) {
  if (isCKCdnTestingVersion(version)) {
    return 3;
  }
  const {
    major
  } = destructureSemanticVersion(version);
  switch (true) {
    case major >= 44:
      return 3;
    case major >= 38:
      return 2;
    default:
      return 1;
  }
}
function getCKBaseBundleInstallationInfo() {
  const {
    CKEDITOR_VERSION,
    CKEDITOR
  } = window;
  if (!isCKCdnVersion(CKEDITOR_VERSION)) {
    return null;
  }
  return {
    source: CKEDITOR ? "cdn" : "npm",
    version: CKEDITOR_VERSION
  };
}
function getSupportedLicenseVersionInstallationInfo() {
  const installationInfo = getCKBaseBundleInstallationInfo();
  if (!installationInfo) {
    return null;
  }
  return getLicenseVersionFromEditorVersion(installationInfo.version);
}
function isCKEditorFreeLicense(licenseKey, licenseVersion) {
  licenseVersion ||= getSupportedLicenseVersionInstallationInfo() || void 0;
  switch (licenseVersion) {
    case 1:
    case 2:
      return licenseKey === void 0;
    case 3:
      return licenseKey === "GPL";
    default: {
      return false;
    }
  }
}
function createIntegrationUsageDataPlugin(integrationName, usageData) {
  return function IntegrationUsageDataPlugin(editor) {
    if (isCKEditorFreeLicense(editor.config.get("licenseKey"))) {
      return;
    }
    editor.on("collectUsageData", (source, {
      setUsageData
    }) => {
      setUsageData(`integration.${integrationName}`, usageData);
    });
  };
}
var CK_CDN_URL = "https://cdn.ckeditor.com";
function createCKCdnUrl(bundle, file, version) {
  return `${CK_CDN_URL}/${bundle}/${version}/${file}`;
}
var CKBOX_CDN_URL = "https://cdn.ckbox.io";
function createCKBoxCdnUrl(bundle, file, version) {
  return `${CKBOX_CDN_URL}/${bundle}/${version}/${file}`;
}
var CK_DOCS_URL = "https://ckeditor.com/docs/ckeditor5";
function createCKDocsUrl(path, version = "latest") {
  return `${CK_DOCS_URL}/${version}/${path}`;
}
function createCKCdnBaseBundlePack({
  version,
  translations,
  createCustomCdnUrl = createCKCdnUrl
}) {
  const urls = {
    scripts: [
      // Load the main script of the base features.
      createCustomCdnUrl("ckeditor5", "ckeditor5.umd.js", version),
      // Load all JavaScript files from the base features.
      // EN bundle is prebuilt into the main script, so we don't need to load it separately.
      ...without(["en"], translations || []).map((translation) => createCustomCdnUrl("ckeditor5", `translations/${translation}.umd.js`, version))
    ],
    stylesheets: [createCustomCdnUrl("ckeditor5", "ckeditor5.css", version)]
  };
  return {
    // Preload resources specified in the pack, before loading the main script.
    preload: [...urls.stylesheets, ...urls.scripts],
    scripts: [
      // It's safe to load translations and the main script in parallel.
      (attributes) => __async(this, null, function* () {
        return injectScriptsInParallel(urls.scripts, attributes);
      })
    ],
    // Load all stylesheets of the base features.
    stylesheets: urls.stylesheets,
    // Pick the exported global variables from the window object.
    checkPluginLoaded: () => __async(this, null, function* () {
      return waitForWindowEntry(["CKEDITOR"]);
    }),
    // Check if the CKEditor base bundle is already loaded and throw an error if it is.
    beforeInject: () => {
      const installationInfo = getCKBaseBundleInstallationInfo();
      switch (installationInfo?.source) {
        case "npm":
          throw new Error("CKEditor 5 is already loaded from npm. Check the migration guide for more details: " + createCKDocsUrl("updating/migration-to-cdn/vanilla-js.html"));
        case "cdn":
          if (installationInfo.version !== version) {
            throw new Error(`CKEditor 5 is already loaded from CDN in version ${installationInfo.version}. Remove the old <script> and <link> tags loading CKEditor 5 to allow loading the ${version} version.`);
          }
          break;
      }
    }
  };
}
function createCKCdnPremiumBundlePack({
  version,
  translations,
  createCustomCdnUrl = createCKCdnUrl
}) {
  const urls = {
    scripts: [
      // Load the main script of the premium features.
      createCustomCdnUrl("ckeditor5-premium-features", "ckeditor5-premium-features.umd.js", version),
      // Load all JavaScript files from the premium features.
      // EN bundle is prebuilt into the main script, so we don't need to load it separately.
      ...without(["en"], translations || []).map((translation) => createCustomCdnUrl("ckeditor5-premium-features", `translations/${translation}.umd.js`, version))
    ],
    stylesheets: [createCustomCdnUrl("ckeditor5-premium-features", "ckeditor5-premium-features.css", version)]
  };
  return {
    // Preload resources specified in the pack, before loading the main script.
    preload: [...urls.stylesheets, ...urls.scripts],
    scripts: [
      // It's safe to load translations and the main script in parallel.
      (attributes) => __async(this, null, function* () {
        return injectScriptsInParallel(urls.scripts, attributes);
      })
    ],
    // Load all stylesheets of the premium features.
    stylesheets: urls.stylesheets,
    // Pick the exported global variables from the window object.
    checkPluginLoaded: () => __async(this, null, function* () {
      return waitForWindowEntry(["CKEDITOR_PREMIUM_FEATURES"]);
    })
  };
}
function loadCKCdnResourcesPack(pack) {
  return __async(this, null, function* () {
    let {
      htmlAttributes = {},
      scripts = [],
      stylesheets = [],
      preload,
      beforeInject,
      checkPluginLoaded
    } = normalizeCKCdnResourcesPack(pack);
    beforeInject?.();
    if (!preload) {
      preload = uniq([...stylesheets.filter((item) => typeof item === "string"), ...scripts.filter((item) => typeof item === "string")]);
    }
    for (const url of preload) {
      preloadResource(url, {
        attributes: htmlAttributes
      });
    }
    yield Promise.all(uniq(stylesheets).map((href) => injectStylesheet({
      href,
      attributes: htmlAttributes,
      placementInHead: "start"
    })));
    for (const script of uniq(scripts)) {
      const injectorProps = {
        attributes: htmlAttributes
      };
      if (typeof script === "string") {
        yield injectScript(script, injectorProps);
      } else {
        yield script(injectorProps);
      }
    }
    return checkPluginLoaded?.();
  });
}
function normalizeCKCdnResourcesPack(pack) {
  if (Array.isArray(pack)) {
    return {
      scripts: pack.filter((item) => typeof item === "function" || item.endsWith(".js")),
      stylesheets: pack.filter((item) => item.endsWith(".css"))
    };
  }
  if (typeof pack === "function") {
    return {
      checkPluginLoaded: pack
    };
  }
  return pack;
}
function combineCKCdnBundlesPacks(packs) {
  const normalizedPacks = mapObjectValues(filterBlankObjectValues(packs), normalizeCKCdnResourcesPack);
  const mergedPacks = Object.values(normalizedPacks).reduce((acc, pack) => {
    acc.scripts.push(...pack.scripts ?? []);
    acc.stylesheets.push(...pack.stylesheets ?? []);
    acc.preload.push(...pack.preload ?? []);
    return acc;
  }, {
    preload: [],
    scripts: [],
    stylesheets: []
  });
  const checkPluginLoaded = () => __async(this, null, function* () {
    const exportedGlobalVariables = /* @__PURE__ */ Object.create(null);
    for (const [name, pack] of Object.entries(normalizedPacks)) {
      exportedGlobalVariables[name] = yield pack?.checkPluginLoaded?.();
    }
    return exportedGlobalVariables;
  });
  const beforeInject = () => {
    for (const pack of Object.values(normalizedPacks)) {
      pack.beforeInject?.();
    }
  };
  return __spreadProps(__spreadValues({}, mergedPacks), {
    beforeInject,
    checkPluginLoaded
  });
}
function getCKBoxInstallationInfo() {
  const version = window.CKBox?.version;
  if (!isSemanticVersion(version)) {
    return null;
  }
  return {
    source: "cdn",
    version
  };
}
function createCKBoxBundlePack({
  version,
  theme = "lark",
  translations,
  createCustomCdnUrl = createCKBoxCdnUrl
}) {
  return __spreadProps(__spreadValues({
    // Load the main script of the base features.
    scripts: [
      createCustomCdnUrl("ckbox", "ckbox.js", version),
      // EN bundle is prebuilt into the main script, so we don't need to load it separately.
      ...without(["en"], translations || []).map((translation) => createCustomCdnUrl("ckbox", `translations/${translation}.js`, version))
    ]
  }, theme && {
    stylesheets: [createCustomCdnUrl("ckbox", `styles/themes/${theme}.css`, version)]
  }), {
    // Pick the exported global variables from the window object.
    checkPluginLoaded: () => __async(this, null, function* () {
      return waitForWindowEntry(["CKBox"]);
    }),
    // Check if the CKBox bundle is already loaded and throw an error if it is.
    beforeInject: () => {
      const installationInfo = getCKBoxInstallationInfo();
      if (installationInfo && installationInfo.version !== version) {
        throw new Error(`CKBox is already loaded from CDN in version ${installationInfo.version}. Remove the old <script> and <link> tags loading CKBox to allow loading the ${version} version.`);
      }
    }
  });
}
function isCKCdnSupportedByEditorVersion(version) {
  if (isCKCdnTestingVersion(version)) {
    return true;
  }
  const {
    major
  } = destructureSemanticVersion(version);
  const licenseVersion = getLicenseVersionFromEditorVersion(version);
  switch (licenseVersion) {
    case 3:
      return true;
    default:
      return major === 43;
  }
}
function combineCdnPluginsPacks(pluginsPacks) {
  const normalizedPluginsPacks = mapObjectValues(pluginsPacks, (pluginPack, pluginName) => {
    if (!pluginPack) {
      return void 0;
    }
    const normalizedPluginPack = normalizeCKCdnResourcesPack(pluginPack);
    return __spreadValues({
      // Provide default window accessor object if the plugin pack does not define it.
      checkPluginLoaded: () => __async(this, null, function* () {
        return waitForWindowEntry([pluginName]);
      })
    }, normalizedPluginPack);
  });
  return combineCKCdnBundlesPacks(normalizedPluginsPacks);
}
function loadCKEditorCloud(config) {
  const {
    version,
    translations,
    plugins,
    premium,
    ckbox,
    createCustomCdnUrl,
    injectedHtmlElementsAttributes = {
      crossorigin: "anonymous"
    }
  } = config;
  validateCKEditorVersion(version);
  const pack = combineCKCdnBundlesPacks(__spreadProps(__spreadValues(__spreadValues({
    CKEditor: createCKCdnBaseBundlePack({
      version,
      translations,
      createCustomCdnUrl
    })
  }, premium && {
    CKEditorPremiumFeatures: createCKCdnPremiumBundlePack({
      version,
      translations,
      createCustomCdnUrl
    })
  }), ckbox && {
    CKBox: createCKBoxBundlePack(ckbox)
  }), {
    loadedPlugins: combineCdnPluginsPacks(plugins ?? {})
  }));
  return loadCKCdnResourcesPack(__spreadProps(__spreadValues({}, pack), {
    htmlAttributes: injectedHtmlElementsAttributes
  }));
}
function validateCKEditorVersion(version) {
  if (isCKCdnTestingVersion(version)) {
    console.warn("You are using a testing version of CKEditor 5. Please remember that it is not suitable for production environments.");
  }
  if (!isCKCdnSupportedByEditorVersion(version)) {
    throw new Error(`The CKEditor 5 CDN can't be used with the given editor version: ${version}. Please make sure you are using at least the CKEditor 5 version 44.`);
  }
}

// node_modules/@ckeditor/ckeditor5-angular/fesm2022/ckeditor-ckeditor5-angular.mjs
function CKEditorComponent_ng_template_0_Template(rf, ctx) {
}
var AngularIntegrationUsageDataPlugin = createIntegrationUsageDataPlugin("angular", {
  version: (
    /* replace-version:start */
    "9.1.0"
  ),
  frameworkVersion: VERSION.full
});
function appendAllIntegrationPluginsToConfig(editorConfig) {
  const extraPlugins = [];
  if (!isCKEditorFreeLicense(editorConfig.licenseKey)) {
    extraPlugins.push(AngularIntegrationUsageDataPlugin);
  }
  return appendExtraPluginsToEditorConfig(editorConfig, extraPlugins);
}
var ANGULAR_INTEGRATION_READ_ONLY_LOCK_ID = "Lock from Angular integration (@ckeditor/ckeditor5-angular)";
var CKEditorComponent = class _CKEditorComponent {
  /**
   * The reference to the DOM element created by the component.
   */
  elementRef;
  /**
   * The constructor of the editor to be used for the instance of the component.
   * It can be e.g. the `ClassicEditorBuild`, `InlineEditorBuild` or some custom editor.
   */
  editor;
  /**
   * The configuration of the editor.
   * See https://ckeditor.com/docs/ckeditor5/latest/api/module_core_editor_editorconfig-EditorConfig.html
   * to learn more.
   */
  config = {};
  /**
   * The initial data of the editor. Useful when not using the ngModel.
   * See https://angular.io/api/forms/NgModel to learn more.
   */
  data = "";
  /**
   * Tag name of the editor component.
   *
   * The default tag is 'div'.
   */
  tagName = "div";
  // TODO Change to ContextWatchdog<Editor, HTMLElement> after new ckeditor5 alpha release
  /**
   * The context watchdog.
   */
  watchdog;
  /**
   * Config for the EditorWatchdog.
   */
  editorWatchdogConfig;
  /**
   * Allows disabling the two-way data binding mechanism. Disabling it can boost performance for large documents.
   *
   * When a component is connected using the [(ngModel)] or [formControl] directives and this value is set to true then none of the data
   * will ever be synchronized.
   *
   * An integrator must call `editor.data.get()` manually once the application needs the editor's data.
   * An editor instance can be received in the `ready()` callback.
   */
  disableTwoWayDataBinding = false;
  /**
   * When set `true`, the editor becomes read-only.
   * See https://ckeditor.com/docs/ckeditor5/latest/api/module_core_editor_editor-Editor.html#member-isReadOnly
   * to learn more.
   */
  set disabled(isDisabled) {
    this.setDisabledState(isDisabled);
  }
  get disabled() {
    if (this.editorInstance) {
      return this.editorInstance.isReadOnly;
    }
    return this.initiallyDisabled;
  }
  /**
   * Fires when the editor is ready. It corresponds with the `editor#ready`
   * https://ckeditor.com/docs/ckeditor5/latest/api/module_core_editor_editor-Editor.html#event-ready
   * event.
   */
  ready = new EventEmitter();
  /**
   * Fires when the content of the editor has changed. It corresponds with the `editor.model.document#change`
   * https://ckeditor.com/docs/ckeditor5/latest/api/module_engine_model_document-Document.html#event-change
   * event.
   */
  change = new EventEmitter();
  /**
   * Fires when the editing view of the editor is blurred. It corresponds with the `editor.editing.view.document#blur`
   * https://ckeditor.com/docs/ckeditor5/latest/api/module_engine_view_document-Document.html#event-event:blur
   * event.
   */
  blur = new EventEmitter();
  /**
   * Fires when the editing view of the editor is focused. It corresponds with the `editor.editing.view.document#focus`
   * https://ckeditor.com/docs/ckeditor5/latest/api/module_engine_view_document-Document.html#event-event:focus
   * event.
   */
  focus = new EventEmitter();
  /**
   * Fires when the editor component crashes.
   */
  error = new EventEmitter();
  /**
   * The instance of the editor created by this component.
   */
  get editorInstance() {
    let editorWatchdog = this.editorWatchdog;
    if (this.watchdog) {
      editorWatchdog = this.watchdog._watchdogs.get(this.id);
    }
    if (editorWatchdog) {
      return editorWatchdog.editor;
    }
    return null;
  }
  /**
   * The editor watchdog. It is created when the context watchdog is not passed to the component.
   * It keeps the editor running.
   */
  editorWatchdog;
  /**
   * If the component is read–only before the editor instance is created, it remembers that state,
   * so the editor can become read–only once it is ready.
   */
  initiallyDisabled = false;
  /**
   * An instance of https://angular.io/api/core/NgZone to allow the interaction with the editor
   * withing the Angular event loop.
   */
  ngZone;
  /**
   * A callback executed when the content of the editor changes. Part of the
   * `ControlValueAccessor` (https://angular.io/api/forms/ControlValueAccessor) interface.
   *
   * Note: Unset unless the component uses the `ngModel`.
   */
  cvaOnChange;
  /**
   * A callback executed when the editor has been blurred. Part of the
   * `ControlValueAccessor` (https://angular.io/api/forms/ControlValueAccessor) interface.
   *
   * Note: Unset unless the component uses the `ngModel`.
   */
  cvaOnTouched;
  /**
   * Reference to the source element used by the editor.
   */
  editorElement;
  /**
   * A lock flag preventing from calling the `cvaOnChange()` during setting editor data.
   */
  isEditorSettingData = false;
  id = uid();
  getId() {
    return this.id;
  }
  constructor(elementRef, ngZone) {
    this.ngZone = ngZone;
    this.elementRef = elementRef;
    this.checkVersion();
  }
  checkVersion() {
    const {
      CKEDITOR_VERSION
    } = window;
    if (!CKEDITOR_VERSION) {
      return console.warn('Cannot find the "CKEDITOR_VERSION" in the "window" scope.');
    }
    const [major] = CKEDITOR_VERSION.split(".").map(Number);
    if (major >= 42 || CKEDITOR_VERSION.startsWith("0.0.0")) {
      return;
    }
    console.warn("The <CKEditor> component requires using CKEditor 5 in version 42+ or nightly build.");
  }
  // Implementing the OnChanges interface. Whenever the `data` property is changed, update the editor content.
  ngOnChanges(changes) {
    if (Object.prototype.hasOwnProperty.call(changes, "data") && changes.data && !changes.data.isFirstChange()) {
      this.writeValue(changes.data.currentValue);
    }
  }
  // Implementing the AfterViewInit interface.
  ngAfterViewInit() {
    this.attachToWatchdog();
  }
  // Implementing the OnDestroy interface.
  ngOnDestroy() {
    return __async(this, null, function* () {
      if (this.watchdog) {
        yield this.watchdog.remove(this.id);
      } else if (this.editorWatchdog && this.editorWatchdog.editor) {
        yield this.editorWatchdog.destroy();
        this.editorWatchdog = void 0;
      }
    });
  }
  // Implementing the ControlValueAccessor interface (only when binding to ngModel).
  writeValue(value) {
    if (value === null) {
      value = "";
    }
    if (this.editorInstance) {
      this.isEditorSettingData = true;
      this.editorInstance.data.set(value);
      this.isEditorSettingData = false;
    } else {
      this.data = value;
      this.ready.pipe((0, import_operators.first)()).subscribe((editor) => {
        editor.data.set(this.data);
      });
    }
  }
  // Implementing the ControlValueAccessor interface (only when binding to ngModel).
  registerOnChange(callback) {
    this.cvaOnChange = callback;
  }
  // Implementing the ControlValueAccessor interface (only when binding to ngModel).
  registerOnTouched(callback) {
    this.cvaOnTouched = callback;
  }
  // Implementing the ControlValueAccessor interface (only when binding to ngModel).
  setDisabledState(isDisabled) {
    if (this.editorInstance) {
      if (isDisabled) {
        this.editorInstance.enableReadOnlyMode(ANGULAR_INTEGRATION_READ_ONLY_LOCK_ID);
      } else {
        this.editorInstance.disableReadOnlyMode(ANGULAR_INTEGRATION_READ_ONLY_LOCK_ID);
      }
    }
    this.initiallyDisabled = isDisabled;
  }
  /**
   * Creates the editor instance, sets initial editor data, then integrates
   * the editor with the Angular component. This method does not use the `editor.data.set()`
   * because of the issue in the collaboration mode (#6).
   */
  attachToWatchdog() {
    const creator = (elementOrData, config2) => {
      return this.ngZone.runOutsideAngular(() => __async(this, null, function* () {
        this.elementRef.nativeElement.appendChild(elementOrData);
        const editor = yield this.editor.create(elementOrData, config2);
        if (this.initiallyDisabled) {
          editor.enableReadOnlyMode(ANGULAR_INTEGRATION_READ_ONLY_LOCK_ID);
        }
        this.ngZone.run(() => {
          this.ready.emit(editor);
        });
        this.setUpEditorEvents(editor);
        return editor;
      }));
    };
    const destructor = (editor) => __async(this, null, function* () {
      yield editor.destroy();
      this.elementRef.nativeElement.removeChild(this.editorElement);
    });
    const emitError = (e) => {
      if (hasObservers(this.error)) {
        this.ngZone.run(() => this.error.emit(e));
      } else {
        console.error(e);
      }
    };
    const element = document.createElement(this.tagName);
    const config = this.getConfig();
    this.editorElement = element;
    if (this.watchdog) {
      this.watchdog.add({
        id: this.id,
        type: "editor",
        creator,
        destructor,
        sourceElementOrData: element,
        config
      }).catch((e) => {
        emitError(e);
      });
      this.watchdog.on("itemError", (_, {
        itemId
      }) => {
        if (itemId === this.id) {
          emitError();
        }
      });
    } else {
      const editorWatchdog = new this.editor.EditorWatchdog(this.editor, this.editorWatchdogConfig);
      editorWatchdog.setCreator(creator);
      editorWatchdog.setDestructor(destructor);
      editorWatchdog.on("error", emitError);
      this.editorWatchdog = editorWatchdog;
      this.ngZone.runOutsideAngular(() => {
        editorWatchdog.create(element, config).catch((e) => {
          emitError(e);
        });
      });
    }
  }
  getConfig() {
    if (this.data && this.config.initialData) {
      throw new Error("Editor data should be provided either using `config.initialData` or `data` properties.");
    }
    const config = __spreadValues({}, this.config);
    const initialData = this.config.initialData || this.data;
    if (initialData) {
      config.initialData = initialData;
    }
    return appendAllIntegrationPluginsToConfig(config);
  }
  /**
   * Integrates the editor with the component by attaching related event listeners.
   */
  setUpEditorEvents(editor) {
    const modelDocument = editor.model.document;
    const viewDocument = editor.editing.view.document;
    modelDocument.on("change:data", (evt) => {
      this.ngZone.run(() => {
        if (this.disableTwoWayDataBinding) {
          return;
        }
        if (this.cvaOnChange && !this.isEditorSettingData) {
          const data = editor.data.get();
          this.cvaOnChange(data);
        }
        this.change.emit({
          event: evt,
          editor
        });
      });
    });
    viewDocument.on("focus", (evt) => {
      this.ngZone.run(() => {
        this.focus.emit({
          event: evt,
          editor
        });
      });
    });
    viewDocument.on("blur", (evt) => {
      this.ngZone.run(() => {
        if (this.cvaOnTouched) {
          this.cvaOnTouched();
        }
        this.blur.emit({
          event: evt,
          editor
        });
      });
    });
  }
  static ɵfac = function CKEditorComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _CKEditorComponent)(ɵɵdirectiveInject(ElementRef), ɵɵdirectiveInject(NgZone));
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _CKEditorComponent,
    selectors: [["ckeditor"]],
    inputs: {
      editor: "editor",
      config: "config",
      data: "data",
      tagName: "tagName",
      watchdog: "watchdog",
      editorWatchdogConfig: "editorWatchdogConfig",
      disableTwoWayDataBinding: "disableTwoWayDataBinding",
      disabled: "disabled"
    },
    outputs: {
      ready: "ready",
      change: "change",
      blur: "blur",
      focus: "focus",
      error: "error"
    },
    standalone: false,
    features: [ɵɵProvidersFeature([{
      provide: NG_VALUE_ACCESSOR,
      // eslint-disable-next-line @typescript-eslint/no-use-before-define
      useExisting: forwardRef(() => _CKEditorComponent),
      multi: true
    }]), ɵɵNgOnChangesFeature],
    decls: 1,
    vars: 0,
    template: function CKEditorComponent_Template(rf, ctx) {
      if (rf & 1) {
        ɵɵtemplate(0, CKEditorComponent_ng_template_0_Template, 0, 0, "ng-template");
      }
    },
    encapsulation: 2
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(CKEditorComponent, [{
    type: Component,
    args: [{
      selector: "ckeditor",
      template: "<ng-template></ng-template>",
      // Integration with @angular/forms.
      providers: [{
        provide: NG_VALUE_ACCESSOR,
        // eslint-disable-next-line @typescript-eslint/no-use-before-define
        useExisting: forwardRef(() => CKEditorComponent),
        multi: true
      }]
    }]
  }], function() {
    return [{
      type: ElementRef
    }, {
      type: NgZone
    }];
  }, {
    editor: [{
      type: Input
    }],
    config: [{
      type: Input
    }],
    data: [{
      type: Input
    }],
    tagName: [{
      type: Input
    }],
    watchdog: [{
      type: Input
    }],
    editorWatchdogConfig: [{
      type: Input
    }],
    disableTwoWayDataBinding: [{
      type: Input
    }],
    disabled: [{
      type: Input
    }],
    ready: [{
      type: Output
    }],
    change: [{
      type: Output
    }],
    blur: [{
      type: Output
    }],
    focus: [{
      type: Output
    }],
    error: [{
      type: Output
    }]
  });
})();
function hasObservers(emitter) {
  return emitter.observed || emitter.observers.length > 0;
}
var CKEditorModule = class _CKEditorModule {
  static ɵfac = function CKEditorModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _CKEditorModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _CKEditorModule,
    declarations: [CKEditorComponent],
    imports: [FormsModule, CommonModule],
    exports: [CKEditorComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [FormsModule, CommonModule]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(CKEditorModule, [{
    type: NgModule,
    args: [{
      imports: [FormsModule, CommonModule],
      declarations: [CKEditorComponent],
      exports: [CKEditorComponent]
    }]
  }], null, null);
})();
export {
  CKEditorComponent,
  CKEditorModule,
  loadCKEditorCloud
};
/*! Bundled license information:

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-integrations-common/dist/index.js:
  (**
   * @license Copyright (c) 2003-2025, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-licensing-options
   *)

@ckeditor/ckeditor5-angular/fesm2022/ckeditor-ckeditor5-angular.mjs:
  (**
   * @license Copyright (c) 2003-2024, CKSource Holding sp. z o.o. All rights reserved.
   * For licensing, see LICENSE.md.
   *)
*/
//# sourceMappingURL=@ckeditor_ckeditor5-angular.js.map
