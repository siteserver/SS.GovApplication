var $apiUrl = '/ss.govapplication/data/add';

var data = {
  siteId: utils.getQueryString('siteId'),
  dataId: utils.getQueryString('dataId'),
  returnUrl: utils.getQueryString('returnUrl'),
  pageConfig: null,
  pageLoad: false,
  pageAlert: null,
  pageType: '',
  dataInfo: {
    isOrganization: false,
    provideType: [],
    obtainType: []
  }
};

var methods = {
  submit: function () {
    var $this = this;

    var payload = {
      siteId: this.siteId,
      dataId: this.dataId
    };
    for (var i = 0; i < this.fieldInfoList.length; i++) {
      var style = this.fieldInfoList[i];
      // if (typeof style.value === 'object') {
      //   style.value = JSON.stringify(style.value);
      // }
      // console.log(style.value);
      payload[style.title] = style.value;
    }

    utils.loading(true);
    $api.post(payload, function (err, res) {
      utils.loading(false);
      if (err) {
        $this.pageAlert = {
          type: 'danger',
          html: err.message
        };
        return;
      }

      alert({
        toast: true,
        type: 'success',
        title: "数据保存成功",
        showConfirmButton: false,
        timer: 2000
      }).then(function () {
        $this.btnNavClick("logs.html");
      });
    });
  },

  btnSubmitClick: function () {
    var $this = this;
    this.pageAlert = null;

    this.$validator.validate().then(function (result) {
      if (result) {
        $this.submit();
      }
    });
  }
};

var $vue = new Vue({
  el: "#main",
  data: data,
  methods: methods,
  created: function () {
    var $this = this;

    $api.get($apiUrl, {
      params: {
        siteId: this.siteId,
        dataId: this.dataId
      }
    }).then(function (response) {
      var res = response.data;

    }).catch(function (error) {
      $this.pageAlert = {
        type: 'danger',
        html: error.response && error.response.data ? error.response.data.message : error.message
      };
    }).then(function () {
      $this.pageLoad = true;
    });
  }
});