//有些方法 想 edit / add  可以不封装
//封装到这里的目的是为了 更改一个全局 所有的都一起更改了

layui.define(['table', 'layer', 'form', 'laypage', 'ajaxmod', 'jquery'], function (exports) {
    var table = layui.table,
        layer = layui.layer,
        form = layui.form,
        laypage = layui.laypage,
        ajaxmod = layui.ajaxmod,
        navbar = layui.navbar,
        $ = layui.jquery;

    var datatable = {
        currTable: {},//当前渲染的table对象
        currParamDefault: {},//当前渲染的table参数
        // _data ajax数据   param table参数
        renderTable: function (_data, _param) {
            var _the = this;
            var optDefault = {
                Page: 1,
                PageSize: 10,
                //查询条件需要前边传过来
                //UAccount: $('#UAccount', _context).val(),
            };
            var paramDefault = {
                url: '/userinfo',
                title: '用户信息',//编辑弹窗的标题
                editArea: ['850px', '500px'],//编辑弹窗的大小
                editView: 'view/userinfo/edit.html',//编辑弹窗的视图地址+参数
                refreshTableEvent:'edit_userinfo_form_refresh',//刷新table 的事件名称
                tableElem: 'table_userinfo',
                tableCols: [],
                done: function (res, curr, count) { },
                success: function (_json) {},
            };
            $.extend(optDefault, _data);
            $.extend(paramDefault, _param);
            _the.currParamDefault = paramDefault;
            //获取数据
            ajaxmod.authorizeAjax({
                url: paramDefault.url,
                data: optDefault,//查询条件
                success: function (_json) {
                    var data=_json.Result;
                    _the.currTable= table.render({
                        elem: '#'+paramDefault.tableElem,
                        cols: paramDefault.tableCols,
                        data: data.data,
                        //height: 'full-200',
                        limit: optDefault.PageSize,
                        done:paramDefault.done
                    });
                    paramDefault.success(_json);
                    _the.renderPage(data.paging, optDefault,paramDefault);
                }
            });
        },
        //_data 数据结果  optDefault加载datatable的参数搜索  paramDefault前台传过来的table参数
        renderPage: function (_data, optDefault, paramDefault) {
            var _the = this;
            laypage.render({
                elem: paramDefault.tableElem+'_page',
                theme: '#FF5722',
                count: _data.TotalCount,
                curr: _data.Page,
                limit: _data.PageSize,
                groups: 10,
                layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
                jump: function (obj, first) {
                    if (!first) {//首次不执行
                        _the.renderTable($.extend(optDefault, { Page: obj.curr, PageSize: obj.limit }), paramDefault);
                    }
                }
            });
            vm.currPage = _data.Page;//给当前vm赋值
            vm.PageSize = _data.PageSize;
        },
        edit: function (_options) {
            var _the = this;
            var optDefault = {
                type: 2,
                shade: 0.6,
                fixed: false,
                maxmin: true,
                title: '编辑用户信息',
                area: ['850px', '500px'],//默认自适应
                content: 'view/userinfo/edit.html?UId=0',
            };
            $.extend(optDefault,_options);
            top.layer.open(optDefault);
        },
        add: function (_options) {
            var _the = this;
            var optDefault = {
                type: 2,
                shade: 0.6,
                fixed: false,
                maxmin: true,
                title: '新增用户信息',
                area: ['850px', '500px'],//默认自适应
                content: 'view/userinfo/edit.html',
            };
            $.extend(optDefault, _options);
            top.layer.open(optDefault);
        },
        del: function (_options) {
            var _the = this;
            var optDefault = {
                url: '/userinfo/0',
                type: 'Delete',
                success: function (_json) {
                    console.log("没有触发事件刷新列表")//触发事件刷新列表
                }
            };
            $.extend(optDefault, _options);
            top.layer.confirm('确认删除吗？', { icon: 3, title: '提示' }, function (index, layero) {
                ajaxmod.authorizeAjax(optDefault);
                top.layer.close(index);
            }, function () { });
        },
        delbatch: function (_options) {
            var _the = this;
            var optDefault = {
                url: '/userinfo',
                type: 'Delete',
                data: {
                    idlist: ['', '']
                },
                success: function (_json) {
                    console.log("没有触发事件刷新列表")//触发事件刷新列表
                }
            };
            $.extend(optDefault, _options);
            top.layer.confirm('确认删除选中项吗？', { icon: 3, title: '提示' }, function (index, layero) {
                ajaxmod.authorizeAjax(optDefault);
                top.layer.close(index);
            }, function () { });
        },
        renderTime: function (val) {
            if (val === '0001-01-01 00:00:00') {
                return '';
            }
            return val;
        }
    };

    exports('datatable', datatable);
});