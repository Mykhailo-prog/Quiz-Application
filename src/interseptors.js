import axios from "axios";
import store from "./store";

export default {
  register() {
    axios.interceptors.request.use(
      (request) =>
        new Promise((resolve) => {
          resolve(request);
        })
    );

    axios.interceptors.response.use(
      (response) => {
        return response;
      },
      async (err) => {
        var res = JSON.parse(err.response.request.response);
        store.commit("SET_RESPONSE", res);

        console.log(res.errors);
      }
    );
  },
};
