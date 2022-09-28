import Vue from "vue";
import Vuex from "vuex";
import users from "./users";
import questions from "./questions";
import tests from "./tests";
import answers from "./answers";
import register from "./register";

Vue.use(Vuex);

const modules = {
  users,
  questions,
  tests,
  answers,
  register,
};

export default new Vuex.Store({
  modules: modules,
});
