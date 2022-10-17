import Vue from "vue";
import App from "./App.vue";
import Router from "./router";
import Store from "./store/index";
import axios from "axios";
import VueAxios from "vue-axios";
import BootstrapVue from "bootstrap-vue";
import { ModalPlugin } from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "./axios";
import Interseptors from "@/interseptors";

Vue.use(BootstrapVue);
Vue.use(VueAxios, axios);
Vue.use(ModalPlugin);
Vue.config.productionTip = false;

Interseptors.register();

new Vue({
  router: Router,
  store: Store,
  render: (h) => h(App),
}).$mount("#app");
