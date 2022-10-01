import TestResult from "@/pages/TestResult";
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
        (test) => test.userCreatedTest[0].userId === getters.Access.user.id
      ),
    CurrentTest: (state) => state.currTest,
    ResultTime: (state) => state.resultTime,
    TestResult: (state) => testResult,
  },
  actions: {
    async loadTests({ commit }) {
      const loadedTests = await axios.get("https://localhost:44378/api/tests");
      commit("SET_TEST_LIST", loadedTests.data);
    },
    setEndTime({ commit }, endTime) {
      commit("END_TIME", endTime);
    },
    chooseTest({ commit, state, dispatch }, id) {
      const test = state.testList.find((t) => t.testId === id);

      commit("SET_CURRENT_TEST", test);
      commit("SET_QUEST_LIST_LENGTH", test.questions);
      commit("START_TIME");
    },

    async delTest({ dispatch }, testId) {
      await axios.delete(
        "https://localhost:44378/api/tests/" + testId.toString()
      );
      await dispatch("loadTests");
    },

    async finishTest({ state, commit, getters }) {
      const res = await axios.put(
        "https://localhost:44378/api/users/" + getters.Access.user.id,
        {
          test: getters.CurrentTest.testId,
          time: state.resultTime,
          userAnswers: getters.Answers,
        }
      );
      commit("SET_TEST_RESULT", res.data);
    },

    async postNewTest({ commit }, payload) {
      const newTest = await axios
        .post("https://localhost:44378/api/tests", payload.test)
        .then((response) => response.data);
      payload.quests.forEach(async (quest) => {
        const newQuest = await axios
          .post("https://localhost:44378/api/questions", {
            question: quest.question,
            correctAnswer: quest.correctAnswer,
            testId: newTest.testId,
          })
          .then((response) => response.data);
        quest.NewAnswers.forEach((ans) => {
          ans.questionId = newQuest.id;
        });
        await axios.post(
          "https://localhost:44378/api/answers",
          quest.NewAnswers
        );
      });
      await axios.post("https://localhost:44378/api/UserTestConnection", {
        testId: newTest.testId,
        userId: payload.user.id,
      });
      await axios.post("https://localhost:44378/api/Statistic", {
        testId: newTest.testId,
      });
    },
  },
};
