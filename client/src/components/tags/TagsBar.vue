<template>
    <v-row>
        <v-col
        v-if="!isLoadingTags"
        class="d-flex align-center justify-end">
            <v-checkbox
            v-for="tag in tags"
            :key="tag.id"
            v-model="selectedTags"
            multiple
            indeterminate-icon="mdi-eye-outline"
            :label="tag.name"
            color="indigo"
            :value="tag.id"
            hide-details
            class="ml-3"
            on-icon="mdi-eye-check-outline"
            off-icon="mdi-eye-outline"
            @change="$emit('tags-selected',selectedTags)"
            :indeterminate.sync="stateIndetermianete"
            ></v-checkbox>

            <v-btn
            v-if="tags.length>0"
            class="ml-5 mt-5"
            icon
            small
            title="usuń filtry"
            @click="clear">
                <v-icon>mdi-filter-remove-outline</v-icon>
            </v-btn>
        </v-col>
        <v-col v-else class="text-right">
            <v-progress-circular
            indeterminate
            class="mt-5"
            >
        </v-progress-circular>
        </v-col>
    </v-row>
</template>
<script>
    export default{
        name: 'TagsBar',
        data(){
            return{
                tags: [],
                selectedTags: [],
                isLoadingTags: true,
                isCleared: true,
                stateIndetermianete: true
            }
        },
        methods:{
            async getTags(){
                const response = await this.$http.get('Tags/GetAll').catch((response)=>{response})

                if(response && response.data.status===true){
                    this.tags = response.data.data
                }

                this.isLoadingTags = false
            },
            clear(){
                this.selectedTags=[]
                this.stateIndetermianete = true
                this.$emit('tags-selected',this.selectedTags)
            }
        },
        mounted(){
            this.getTags()
        }
    }
</script>