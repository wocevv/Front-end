<script>

    let myString = "";
    import linegraph from '../../src/components/Linegraph.vue'
    import bargraph from '../../src/components/Barchart.vue'
    import router from '@/router'
    import { defineComponent, ref, watchEffect } from 'vue'
    let Datamodel = ""
    let ShowWhatData = ""
    let GroupBy = ""
    let Datarange = ""
    let ChosenGraph = ""
   const widgets = ref([])
    export default {

            components: {
            linegraph,
            bargraph
            },
        data() {
            return {
                test: true,
                listItems: []
            }
        },
        methods: {
            async getData() {
                const res = await fetch("https://localhost:5001/api/widget/category");
                const finalRes = await res.json();
                this.listItems = finalRes;
            },
            Gonext(id) {
                myString = id   
                router.push({ name: 'test', params: { myString } })             
            }
        },
        mounted() {
            this.getData()
        },

        props: {
            selectedOption: {
                type: String,
                required: true
            },
            selectedChart: {
                type: String,
                required: true
            }
        },
        setup(props) {
            if (props.selectedOption === null ) {
                selectedOption = 'undefined'
            }
            if (props.selectedChart === null ) {
                selectedChart = 'undefined'
            }
        }
    }


</script>

<script setup>

    function SetWidgetConfig() {
        const temp = ref([Datamodel, ShowWhatData, GroupBy])
        widgets.value.push(temp.value)
        console.log(widgets)
        // widgets.push()
    }
    
</script>

<template>
    <div>
        <button @click="test = !test">+</button>

        <div v-if="!test">
            <img @click="ChosenGraph = 'line-chart'" src="../../src/images/line-chart.png" width="240" height="128" />
            <img @click="ChosenGraph = 'bar-chart'" src="../../src/images/barchart.jpg" width="240" height="128" />
            <br />
            <select name="category" id="category" v-model="Datamodel">
                <option disabled selected hidden value=""> Please choose what Datamodel u want to use</option>
                <option v-for="item in listItems">{{item}}</option>
            </select>
            <br />
            <select name="category" id="category" v-model="GroupBy">
                <option disabled selected hidden value="">Please choose what u want to sort by</option>
                <option>Day</option>
                <option>Week</option>
                <option>Month</option>
                <option>Year</option>
            </select>
            <br />
            <select name="category" id="category" v-model="ShowWhatData">
                <option disabled selected hidden value="">Please choose what kinf of data u want to visualise</option>
                <option>Count</option>
            </select>
            <br />
            <button @click="SetWidgetConfig()">AddWidget</button>
        </div>
        <div>

        </div>
    </div>

    <div v-if="selectedChart === 'line-chart'">
        <div>{{selectedOption}}</div>
        <linegraph :my-string="selectedOption"></linegraph>
    </div>
    <div v-if="selectedChart === 'bar-chart'">
        <div>{{selectedOption}}</div>
        <bargraph :my-string="selectedOption"></bargraph>
    </div>
</template>
