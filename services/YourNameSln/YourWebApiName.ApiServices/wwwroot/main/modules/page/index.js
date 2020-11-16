﻿/**
 * Name:app.js
 * Author:Anspray
 * LICENSE:MIT
window.sessionStorage.setItem("lockcms", true);  没打开一个页面一个session 会话结束
       localStorage.setItem("rememberme", JSON.stringify(userinfo)); 全局有效
 */

//新增 pajax 无状态刷新页面
var vm = new Vue({
    el: '#page-wrapper',
    data: {
        navList: [],
        userinfo: {}
    },
    methods: {
        RenderDom: function(form){
            vm.$forceUpdate();//强制重新渲染 dom
            // DOM 还没有更新
            vm.$nextTick(function () {
                // DOM 现在更新了
                form.render();
            });
        },
        //加载菜单
        loadMenu: function (event) {
            var _the = this;
            layui.use(['jquery', 'navbar', 'tab'], function () {
                var navbar = layui.navbar,
                    tab = layui.tab,
                    $ = layui.jquery;
                if (event) {
                    var id = event.target.getAttribute('data-code');
                    $.each(_the.navList, function (k, v) {
                        if (id === v.id) {
                            navbar.set({
                                data: v.children
                            }).render(function (data) {
                                tab.tabAdd(data);
                            });
                            return false;
                        }
                    });
                }

            });
        }
    }
});

var tab;
layui.define(['tab', 'navbar', 'jquery', 'form', 'layer', 'ajaxmod'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer,
        navbar = layui.navbar,
        ajaxmod = layui.ajaxmod,
        form = layui.form;
    tab = layui.tab;


    ajaxmod.validateLogin();//验证是否登录了
    ajaxmod.GetUserInfo(function (_json) {
        vm.$set(vm, 'userinfo', _json.Result);
    });

    //验证是否锁屏
    if (localStorage.getItem("lockcms") === "true") {
        tab.lockPage();//锁屏
    }

    // 转换
    //var parameter = $.param(layui.device());

    //主页面对象
    var pageHome = {
        searchMenuList: [],//搜索菜单列表 只包含带链接的 可点击的
        changePage: function (data) {
            console.log(data);
            data = data === undefined ? {
                icon: "",
                id: "1tab1",
                title: "没有传入标题",
                url: mainUrl
            } : data;
            layer.load(2, {
                shade: [0.2, "black"]
            });
            setTimeout(function () {
                layer.closeAll('loading');
                tab.tabAdd(data);
            }, 1000);
        },
        // 修改密码
        resetpassword: function () {
            var data = {
                icon: "",
                id: "1tab1",
                title: "修改密码",
                url: "view/other/resetpassword.html"
            };
            pageHome.changePage(data);
        },
        // 退出
        loginout: function () {
            cookie.clear();
            window.location.href = "/login.html";
        },
        //获取菜单
        initMenuList: function (_options) {
            var _the = this;
            ajaxmod.authorizeAjax({
                url: '/androleandmodule',
                data:{Oprator:10},
                type: 'Get',
                success: function (_json) {
                    vm.$set(vm, 'navList', _json.Result);
                    if (_json.Result.length<1) {
                        console.log("没有任何权限");
                        return false;
                    }
                    navbar.set({
                        data: _json.Result[0].children//初始化菜单 默认使用第一个
                    }).render(function (data) {
                        console.log(data);
                        tab.tabAdd(data);
                    });

                    vm.RenderDom(form);
                    _the.updateMenuList();
                }
            });
        },
        //更新便捷菜单
        updateMenuList: function () {
            var _the = this;
            var str = '<option value="">便捷菜单查询</option>';

            var getChild = function (_leaf) {
                if (_leaf.IsLeaf === 1) {
                    _leaf.children.forEach(function (i) {
                        getChild(i);
                    });
                }
                else if (_leaf.url.length > 1) {
                    _the.searchMenuList.push(_leaf);
                    str += '<option value=“'+_leaf.id+'”>'+_leaf.title+'</option>';
                    
                }
            };
            vm.navList.forEach(function (v) {
                if (v.is_module === 1) {
                    v.children.forEach(function (i) {
                        getChild(i);
                    });
                }
                else if (v.url && v.url.length > 1) {
                    _the.searchMenuList.push(v);
                    str += '<option value=${v.id}>${v.title}</option>';
                }
            });
            $("#search-menu").html(str);
            form.render();
        }
    };

    var app = {
        config: {
            type: 'iframe',
            // 设置是否刷新
            isrefresh: false
        },
        set: function (options) {
            var that = this;
            $.extend(true, that.config, options);
            return that;
        },
        init: function () {
            var that = this,
                _config = that.config;
            if (_config.type === 'iframe') {
                tab.set({
                    elem: '#container',
                    onSwitch: function (data) {
                        if (that.config.isrefresh) {
                            var src = $(".layui-tab-content").find(".layui-show iframe").attr("src");
                            $(".layui-tab-content").find(".layui-show iframe").attr("src", src);
                        }
                        //选项卡切换时触发
                        //lay-id值
                        //console.log(data.layId);
                        //得到当前Tab的所在下标
                        //console.log(data.index);
                        //得到当前的Tab大容器
                        //console.log(data.elem);
                    },
                    closeBefore: function (data) {
                        //关闭选项卡之前触发
                        //显示的图标
                        //console.log(data);
                        //console.log(data.icon);
                        //lay-id
                        //console.log(data.id);
                        //显示的标题
                        //console.log(data.title);
                        //跳转的地址
                        //console.log(data.url);
                        return true; //返回true则关闭
                    }
                }).render();
            }

            pageHome.initMenuList();
            return that;
        }
    };

    //解锁
    $("#lock-box").on("click", '#unlock', function () {
        var _the = this;
        var _lock_pwd = $('#lock-box #lockPwd').val();
        if (!_lock_pwd || _lock_pwd === '') {
            layer.msg("请输入登陆密码进行解锁！");
        } else {
            if (_lock_pwd=='123456'){
                $('#lock-box #lockPwd').val('');
                localStorage.setItem("lockcms", false);
                layer.closeAll("page");
            }else{
                layer.msg("密码错误，请重新输入!");
            }
        }
    });

    //菜单事件
    $('#page-wrapper .layui-action').on('click', function () {
        var type = $(this).data('type');
        pageHome[type] ? pageHome[type].call(this) : '';
    });

    // 便捷查询菜单事件
    form.on('select(select-search)', function (data) {
        pageHome.searchMenuList.forEach(function (e) {
            if (e.id === data.value) {
                pageHome.changePage(e);
            }
        });
    });

    //皮肤
    var setSkin = function (value) {
            layui.data('kit_skin', {
                key: 'skin',
                value: value
            });
        },
        getSkinName = function () {
            return layui.data('kit_skin').skin;
        },
        switchSkin = function (value) {
            var _target = $('link[kit-skin]')[0];
            _target.href = _target.href.substring(0, _target.href.lastIndexOf('/') + 1) + value + _target.href.substring(_target.href.lastIndexOf('.'));
            setSkin(value);
        },
        initSkin = function () {
            var skin = getSkinName();
            switchSkin('default');
            // switchSkin(skin === undefined ? 'default' : skin);
        }();

    // 皮肤
    $('#color').click(function () {
        layer.open({
            type: 1,
            title: '更换皮肤',
            area: ['290px', 'calc(100% - 52px)'],
            offset: 'rb',
            shadeClose: true,
            id: 'colors',
            anim: 2,
            shade: 0.2,
            closeBtn: 0,
            isOutAnim: false,
            resize: false,
            move: false,
            skin: 'color-class',
            btn: ['主题蓝','橘子橙', '原谅绿', '少女粉', '天空蓝', '枫叶红'],
            yes: function (index, layero) {
                switchSkin('default');
                return false;
            },
            btn2: function (index, layero) {
                switchSkin('orange');
                return false;
            },
            btn3: function (index, layero) {
                switchSkin('green');
                return false;
            },
            btn4: function (index, layero) {
                switchSkin('pink');
                return false;
            },
            btn5: function (index, layero) {
                switchSkin('blue');
                return false;
            },
            btn6: function (index, layero) {
                switchSkin('red');
                return false;
            }
        });
    });

    // 添加标签
    $('#tag').click(function () {
        var tag = localStorage.getItem("tag");
        layer.prompt({
            formType: 2,
            anim: 1,
            offset: ['52px', 'calc(100% - 500px)'],
            value: tag,
            title: '便签',
            skin: 'demo-class',
            area: ['280px', '160px'],
            id: 'remember', //设定一个id，防止重复弹出
            btn: ['保存', '清空'],
            shade: 0,
            moveType: 1, //拖拽模式，0或者1
            btn2: function (index, layero) {
                localStorage.removeItem("tag");
                $('#remember textarea').val('');
                return false;
            }
        }, function (value, index, elem) {
                var jsonobj = {
                    time: new Date(),
                    value: value
                };
            localStorage.setItem("tag", value);
            layer.close(index);
        });
    });

    // 版本信息
    $('#copyright').click(function () {
        // 调用ajaxfun
        layer.open({
            type: 1,
            title: '版本信息',
            area: ['290px', 'calc(100% - 52px)'],
            offset: 'rb',
            shadeClose: true,
            id: 'copyrights',
            anim: 2,
            shade: 0.2,
            closeBtn: 0,
            isOutAnim: false,
            resize: false,
            move: false,
            skin: 'color-class',
            content: '内容'
        });
    });

    //初始化app 设置
    app.set({
        // 设置切换是否刷新
        isrefresh: false
    }).init();

    $.extend(app, pageHome);

    exports('app', app);
});