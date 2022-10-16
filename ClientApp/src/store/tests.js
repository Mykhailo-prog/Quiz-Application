import TestResult from "ClientApp/src/pages/TestResult";
import axios from "axios";

export default {
  state: {
    testList: [],
    currTest: {},
    start: [],
    end: [],
    resultTime: "",
    testResult: {},
  },
  mutations: {
    END_TIME(state, endTime) {
      state.end = [new Date().getMinutes(), new Date().getSeconds()];
      state.resultTime = `${
        state.end[1] > state.start[1]
          ? state.end[0] - state.start[0]
          : state.end[0] - state.start[0] - 1
      }:${
        state.end[1] > state.start[1]
          ? state.end[1] - state.start[1]
          : Math.abs(state.start[1] - 60) + state.end[1]
      }`;
    },
    SET_TEST_LIST(state, tests) {
      state.testList = tests;
    },
    SET_CURRENT_TEST(state, test) {
      state.currTest = test;
    },
    SET_NEW_TEST_ID(state, test) {
      state.newTestId = test.testId;
    },
    START_TIME(state) {
      state.start = [new Date().getMinutes(), new Date().getSeconds()];
    },
    SET_TEST_RESULT(state, res) {
      state.testResult = res;
    },
  },
  getters: {
    Tests: (state) => state.testList,
    UserTests: (state, getters) =>
      state.testList.filter(
        (test) =>
          (test.userCreatedTest.length === 0
            ? null
            : test.userCreatedTest[0].quizUserId) === getters.CurrentUser.id
      ),
    CurrentTest: (state) => state.currTest,
    TestResult: (state) => state.testResult,
  },
  actions: {
    async loadTests({ commit }) {
      const loadedTests = await axios.get("tests");
      commit("SET_TEST_LIST", loadedTests.data);
    },
    setEndTime({ commit }, endTime) {
      commit("END_TIME", endTime);
    },
    chooseTest({ commit, state }, id) {
      const test = state.testList.find((t) => t.testId === id);

      commit("SET_CURRENT_TEST", test);
      commit("SET_QUEST_LIST_LENGTH", test.questions);
      commit("START_TIME");
    },

    async delTest({ dispatch }, testId) {
      await axios.delete("tests/" + testId.toString());
      await dispatch("loadTests");
    },

    async finishTest({ state, commit, getters }) {
      const res = await axios.put("users/" + getters.Access.user.id, {
        test: getters.CurrentTest.testId,
        time: state.resultTime,
        userAnswers: getters.Answers,
      });
      commit("SET_TEST_RESULT", res.data);
    },

    async postNewTest({ commit }, payload) {
      await axios.post("tests", payload.test, {
        params: { id: payload.user.id },
      });
    },
    async updateTest({ state }, payload) {
      await axios.put("tests", payload.test, {
        params: {
          id: payload.testId,
        },
      });
    },
  },
};
