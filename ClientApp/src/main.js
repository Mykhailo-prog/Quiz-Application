import Vue from "vue";
import App from "./App.vue";
import Router from "./router";
import Store from "./store/index";
import axios from "axios";
import VueAxios from "vue-axios";
import BootstrapVue from "bootstrap-vue";
import PubNubVue from "pubnub-vue";
import { ModalPlugin } from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "./axios";
import Interseptors from "@/interseptors";

Vue.use(PubNubVue, {
  subscribeKey: "sub-c-ca365b16-2ef0-4595-93dd-80bdc73e00c0",
  publishKey: "pub-c-ec582826-6ede-4f44-a310-09aa38fdab05",
  uuid: "pn-6ab788e3-b5fd-4140-8f8b-a0caedd3d7a4",
  secretKey: "sec-c-MmExNzg3ZTEtNGNjOC00NTlkLWFmOWMtMzE0NGM4YzY0MWI0",
  restore: true,
});

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
