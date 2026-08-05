## 更新

- 添加：新增土耳其语界面及内置插件翻译
- 添加：截图 OCR、OCR 窗口与图片翻译支持独立设置 OCR 识别语种
- 添加：OpenAI 翻译插件支持在 Chat Completions 与 Responses API 间切换
- 添加：新增内置服务图标库，可搜索并为服务快速选择常用品牌图标
- 优化：翻译结果操作按钮采用延迟显示与过渡动画
- 优化：主窗口前台激活策略，确保 Ctrl+C+C 翻译窗口可靠置前并正确处理失焦隐藏
- 优化：WebDAV 备份显示上传状态与结果，并使用全局 HTTP 超时设置
- 优化：应用更新和更新日志改为从 `sunnyx11/STranslate` 获取
- 修复：并发或连续翻译时隔离请求状态，避免已取消请求覆盖新结果并允许立即发起替代请求
- 修复：内置 Google 翻译请求失败
- 修复：关闭设置窗口时密码字段内容丢失
- 修复：OCR 与图片翻译保存图片时未保存当前显示内容及译文覆盖层
- 修复：智能分段未正确拆分编号列表
- 修复：OCR 结果缺少坐标信息时显示非必要警告

## 其他

- [插件市场](https://stranslate.zggsong.com/plugins.html)
- [使用说明](https://stranslate.zggsong.com/docs/)
- [集成调用](https://stranslate.zggsong.com/docs/invoke.html)
- [安装卸载](https://stranslate.zggsong.com/docs/(un)install.html)
- [FAQ](https://stranslate.zggsong.com/docs/faq.html)

**完整更新日志:** [v2.0.9...v2.1.0](https://github.com/sunnyx11/STranslate/compare/97e2ed33556437f9847c39797368235d499a8eb8...v2.1.0)
