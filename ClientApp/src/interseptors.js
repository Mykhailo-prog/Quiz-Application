import axios from "axios";
import router from "./router";
import store from "./store";

export default {
  register() {
    axios.interceptors.request.use((config) => {
      if (localStorage.getItem("token")) {
        config.headers = {
          Authorization: "Bearer " + localStorage.getItem("token"),
        };
      }
      return config;
    });

    axios.interceptors.response.use(
      (config) => {
        if (localStorage.getItem("token")) {
          config.headers = {
            Authorization: "Bearer " + localStorage.getItem("token"),
          };
        }
        return config;
      },
      (err) => {
        if (err.response.status === 401) {
          router.push("/");
          alert("You are not logged in!");
        }
        var res = JSON.parse(err.response.request.response);
        store.commit("SET_RESPONSE", res);
      }
    );
  },
};
