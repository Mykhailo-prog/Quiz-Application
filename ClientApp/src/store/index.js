import Vue from "vue";
import Vuex from "vuex";
import users from "./users";
import questions from "./questions";
import tests from "./tests";
import answers from "./answers";
import errors from "./errors";
import authentication from "./auth";

Vue.use(Vuex);

const modules = {
  users,
  questions,
  tests,
  answers,
  errors,
  authentication,
};

export default new Vuex.Store({
  modules: modules,
});
