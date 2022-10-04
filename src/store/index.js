import Vue from "vue";
import Vuex from "vuex";
import users from "./users";
import questions from "./questions";
import tests from "./tests";
import answers from "./answers";
import register from "./register";
import errors from "./errors";
import reset from "./resetpassword";
import testForm from "./testForm";
import store from "@/store/index.js";

Vue.use(Vuex);

const modules = {
  users,
  questions,
  tests,
  answers,
  register,
  errors,
  reset,
  testForm,
};

export default new Vuex.Store({
  modules: modules,
});
